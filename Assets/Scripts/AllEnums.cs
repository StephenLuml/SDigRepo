using System;

public class AllEnums { }

[Serializable]
public enum ActionState
{
    Idle,
    Digging,
    Moving,
    InMenu
}

[Serializable]
public enum GridBlockType
{
    Empty,
    Block,
    Shop
}

[Serializable]
public enum UpgradeType
{
    Drill,
    Energy,
    Speed
}