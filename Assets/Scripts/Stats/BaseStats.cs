﻿public class BaseStats {

    int attack;
    private int bonusAttack;
    private int baseAttack;
    int defence;
    private int bonusDefence;
    private int baseDefence;
    int speed;
    private int bonusSpeed;
    private int baseSpeed;
    int moral;
    private int bonusMoral;
    private int baseMoral;
    int luck;
    private int bonusLuck;
    private int baseLuck;

    public int BonusAttack
    {
        get { return bonusAttack; }
        set { bonusAttack = value; }
    }

    public int BaseAttack
    {
        get { return baseAttack; }
        set { baseAttack = value; }
    }

    public int BonusDefence
    {
        get { return bonusDefence; }
        set { bonusDefence = value; }
    }

    public int BaseDefence
    {
        get { return baseDefence; }
        set { baseDefence = value; }
    }

    public int BonusSpeed
    {
        get { return bonusSpeed; }
        set { bonusSpeed = value; }
    }

    public int BaseSpeed
    {
        get { return baseSpeed; }
        set { baseSpeed = value; }
    }

    public int BonusMoral
    {
        get { return bonusMoral; }
        set { bonusMoral = value; }
    }

    public int BaseMoral
    {
        get { return baseMoral; }
        set { baseMoral = value; }
    }

    public int BonusLuck
    {
        get { return bonusLuck; }
        set { bonusLuck = value; }
    }

    public int BaseLuck
    {
        get { return baseLuck; }
        set { baseLuck = value; }
    }

    public int Attack
    {
        get
        {
            return attack;
        }

        set
        {
            attack = value;
        }
    }

    public int Defence
    {
        get
        {
            return defence;
        }

        set
        {
            defence = value;
        }
    }

    public int Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public int Moral
    {
        get
        {
            return moral;
        }

        set
        {
            moral = value;
        }
    }

    public int Luck
    {
        get
        {
            return luck;
        }

        set
        {
            luck = value;
        }
    }

    public BaseStats(int bonusAttack, int baseAttack, int bonusDefence, int baseDefence, int bonusSpeed, int baseSpeed, int bonusMoral, int baseMoral, int bonusLuck, int baseLuck)
    {
        this.bonusAttack = bonusAttack;
        this.baseAttack = baseAttack;
        this.bonusDefence = bonusDefence;
        this.baseDefence = baseDefence;
        this.bonusSpeed = bonusSpeed;
        this.baseSpeed = baseSpeed;
        this.bonusMoral = bonusMoral;
        this.baseMoral = baseMoral;
        this.bonusLuck = bonusLuck;
        this.baseLuck = baseLuck;
        updateStats();
    }

    public BaseStats(int baseAttack, int baseDefence, int baseSpeed, int baseMoral, int baseLuck)
    {
        this.baseAttack = baseAttack;
        this.baseDefence = baseDefence;
        this.baseSpeed = baseSpeed;
        this.baseMoral = baseMoral;
        this.baseLuck = baseLuck;
        bonusLuck = bonusAttack = bonusDefence = bonusMoral = bonusSpeed = 0;
        updateStats();
    }

    public void updateStats()
    {
        attack = baseAttack+bonusAttack;
        defence = baseDefence+bonusDefence;
        speed = baseSpeed+bonusSpeed;
        moral = baseMoral+bonusMoral;
        luck = baseLuck+bonusLuck;
    }
}
