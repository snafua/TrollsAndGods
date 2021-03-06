﻿using System;
using MapGenerator;
using UnityEngine;

/// <summary>
/// Movement manager controls movement of a hero. This happens on the overworld map.
/// This class is the result of separating the movement from the gamemanager 
/// Update method.
/// 
/// </summary>
public class MovementManager
{
    public enum states
    {
        Walking,
        stop
    }

    private states state;
    private Player activePlayer;
    public Hero activeHero;
    AStarAlgo aStar;

    private Point startPosition;
    public  Point StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }

    // Values to use with WALK()
    private Reaction[,] reactions;

    private Reaction curReaction;
    private int[,] canWalk;

    public int stepNumber;
    public int totalTilesToBeWalked;
    private GameManager gameManager;
    private Point previousStep;

    // -------- FLAGS -------- \\

    // Flag that allows update to use the movement functions:
    private bool activated;
    public bool Activated
    {
        get { return activated; }
        set { activated = value; }
    }

    // Flag that cancels the movement inbetween steps.
    private bool canceledMovement;
    public bool CanceledMovement
    {
        get { return canceledMovement; }
        set { canceledMovement = value; }
    }

    public MovementManager(Reaction[,] reactions, int[,] canWalk, AStarAlgo aStar, GameManager gm)
    {
        this.aStar = aStar;
        this.gameManager = gm;

        // MAPS:
        this.reactions = reactions;
        this.canWalk = canWalk;
    }

    /// <summary>
    /// Activates the movement. This must happen after a "PrepareMovement( );" has occurred.
    /// </summary>
    public void Activate()
    {
        canceledMovement = false;
        activated = true;
    }

    /// <summary>
    /// Deactivates movement. This ends movement overall. Can be used mid-movement.
    /// </summary>
    public void Deactivate()
    {
        canceledMovement = true;
    }

    /// <summary>
    /// Prepares for movement to a given location. This sets a list of points
    /// that a given hero will traverse. To execute the given movement, use
    /// HasNextStep() -> NextStep() iterate methods.
    /// </summary>
    /// <param name="target"> The new desired postition to move to. </param>
    /// <param name="activeHero"> The hero that will move to this given new location. </param>
    public void PrepareMovement( Point target, Hero hero )
    {
        // Resetting variables:
        stepNumber = 0;
        
        // Setting the active hero:
        this.activeHero = hero;
        this.startPosition = hero.Position;

        previousStep = startPosition;
        // Calculating fastest route from active hero to target:
        activeHero.Path = aStar.calculate(activeHero.Position, target);

        // Calculate total tiles that the hero can walk:
        totalTilesToBeWalked = Math.Min(activeHero.Path.Count, activeHero.CurMovementSpeed);
    }

    /// <summary>
    /// Checks if there are more steps to take.
    /// </summary>
    /// <returns>false if there is no more steps to take. true otherwise.</returns>
    public bool HasNextStep()
    {
        if (canceledMovement) // Cannot happen at first step.
        {
            WalkFinished(previousStep);
            return false;
        }

        if (activeHero.Path.Count > 0)
        {
            if (StopForPreReact(new Point(activeHero.Path[0])))
            {
                activeHero.CurMovementSpeed--;
                WalkFinished(previousStep, new Point(activeHero.Path[0]));
                return false;
            }
        }
        return stepNumber != totalTilesToBeWalked;
    }

    /// <summary> Executes the actual step logically. </summary>
    /// <returns> The next step in the logical positions </returns>
    public Point NextStep()
    {
        // Getting next step from the path:
        Point nextStep = new Point(activeHero.Path[0]);

        // When this step is the last step or the hero should stop before the next step is taken:
        if (IsLastStep(stepNumber + 1))
        {
            WalkFinished(nextStep);
        }

        activeHero.Path.RemoveAt(0);
        stepNumber++;
        activeHero.CurMovementSpeed--;

        // Saving the last step taken:
        previousStep = nextStep;

        // Return logial position to graphics or event
        return nextStep;
    }

    /// <summary>Checks if there is a preReact in the next step.</summary>
    /// <param name="nextStep">The next step</param>
    /// <returns>True if there is a pre-react and the hero should stop,false otherwise.</returns>
    private bool StopForPreReact(Point nextStep)
    {
        int x = nextStep.x;
        int y = nextStep.y;

        if (reactions[x, y] != null)
        {
            if (reactions[x, y].GetType().Equals(typeof(DwellingReact)) ||
                reactions[x, y].GetType().Equals(typeof(CastleReact)) || reactions[x, y].GetType().Equals(typeof(ResourceBuildingReaction)))
            {
                if (reactions[x, y].HasPreReact())
                {
                    return true; // Stop for pre react
                }
            }
            else
            {
                return true; // Stop for pre react
            }
        }
        return false; // Do not stop for pre react
    }

    /// <summary>
    /// Performs the actual reaction:
    /// </summary>
    /// <param name="end"></param>
    private void react(Point end)
    {
        int x = end.x;
        int y = end.y;
        Point start = startPosition;
        
        bool heroNotDead = true;

        // If tile is threatened, perform the additional reaction before the main one
        if (reactions[x, y].HasPreReact())
        {
            // If reaction is castleReact, trigger castleReact instead of preReact if enemy castle
            if (reactions[x, y].GetType() == typeof(CastleReact))
            {
                CastleReact cr = (CastleReact) reactions[x, y];
                if (!cr.Castle.Player.equals(activePlayer))
                {
                    reactions[x, y].React(activeHero);
                    curReaction = reactions[x, y];
                }
                else
                {
                    reactions[x, y].PreReact(activeHero);
                    curReaction = reactions[x, y];
                }
            }
            else
            {
                reactions[x, y].PreReact(activeHero);
                curReaction = reactions[x, y];
            }
            Debug.Log(reactions[x, y].PreReaction + " - prereact");
        }
        // Perform main reaction if there is not a preReaction
        else if (reactions[x, y].React(activeHero))
        {
            Debug.Log(reactions[x, y] + " - react");
            //bool react = reactions[x, y].React(activeHero);

            if (reactions[x, y].GetType().Equals(typeof(ResourceReaction)))
            {
                // TODO visually remove picked up resource
            }
            else if (reactions[x, y].GetType().Equals(typeof(ArtifactReaction)))
            {
                // TODO visually remove picked up artifact
            }
            else if (reactions[x, y].GetType().Equals(typeof(CastleReact)))
            {
                // TODO change owner of defenseless castle visually
                CastleReact cr = (CastleReact) reactions[x, y];
                cr.Castle.changeCastleOwner(cr, gameManager.Players[gameManager.WhoseTurn], gameManager.activeHero);
            }
            else if (reactions[x, y].GetType().Equals(typeof(DwellingReact)))
            {
                // TODO visually dwelling has been captured
            }
            else if (reactions[x, y].GetType().Equals(typeof(ResourceBuildingReaction)))
            {
                // TODO visually resourceBuilding has been captured
            }
        }
        else
        {
            curReaction = reactions[x, y];
        }
    }

    /// <summary>
    /// Called upon ended walk, flips the reactions in fromposition and toposition
    /// </summary>
    /// <param name="end">end position</param>
    public void UpdateReact(Point end)
    {
        Point start = startPosition;
        if (!start.Equals(end))
        {
            // Remove hero from town if he walked out of it
            if (reactions[start.x, start.y].GetType().Equals(typeof(CastleReact)))
            {
                CastleReact cr = (CastleReact)reactions[start.x, start.y];
                cr.Castle.Town.VisitingHero = null;
                cr.Castle.Town.VisitingUnits = new UnitTree();
            }

            // If destination has reaction, set prereact
            if (reactions[end.x, end.y] != null)
            {
                // if you came from a prereact
                if (!reactions[start.x, start.y].GetType().Equals(typeof(HeroMeetReact)))
                    reactions[end.x, end.y].PreReaction = reactions[start.x, start.y].PreReaction;
                else
                    reactions[end.x, end.y].PreReaction = reactions[start.x, start.y];
                reactions[end.x, end.y].PreReaction.Pos = new Point(end.x, end.y);
            }
            // Else, set destination reaction to the heroreaction, and make the tile a triggertile
            else
            {
                // if you came from a prereact
                if (!reactions[start.x, start.y].GetType().Equals(typeof(HeroMeetReact)))
                    reactions[end.x, end.y] = reactions[start.x, start.y].PreReaction;
                else
                    reactions[end.x, end.y] = reactions[start.x, start.y];
                reactions[end.x, end.y].Pos = new Point(end.x, end.y);

                canWalk[end.x, end.y] = MapMaker.TRIGGER;
            }

            // If from position didn't have prereact, flip canwalk and remove
            if (reactions[start.x, start.y].GetType().Equals(typeof(HeroMeetReact)))
            {
                canWalk[start.x, start.y] = MapMaker.CANWALK;
                reactions[start.x, start.y] = null;
            }
            // Else, remove the prereact
            else
            {
                reactions[start.x, start.y].PreReaction = null;
            }
        }
    }

    /// <summary>
    /// Finishes the walking.
    /// </summary>
    public void WalkFinished( Point lastStep )
    {
        WalkFinished(lastStep, lastStep);
    }

    /// <summary>
    /// Finishes the walking.
    /// </summary>
    public void WalkFinished(Point currentStep, Point lastStep)
    {
        // TODO: IMPLEMENT LOGGING IF NOT EVENT.
        activeHero.Position = currentStep;
        // If there is a trigger at the stop position:
        if (canWalk[lastStep.x, lastStep.y] == MapMaker.TRIGGER)
        {
            // React to the reaction at the last step:
            react(lastStep);
        }
        UpdateReact(currentStep);
    }

    /// <summary>
    /// Determines whether this is the last step of the walk.
    /// </summary>
    /// <returns><c>true</c> if the hero is out of steps or the destination is reached. <c>false</c> otherwise.</returns>
    /// <param name="stepNumber">Step number.</param>
    public bool IsLastStep(int stepNumber)
    {
        return  activeHero.CurMovementSpeed == 0 || stepNumber == totalTilesToBeWalked;
    }
}