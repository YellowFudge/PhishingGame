
/// <summary>
/// The different cues that a mail could possibly have
/// </summary>
public enum CueTypes
{
    Error,
    SenderDomain,
    TooGoodToBeTrue,
    GenericGreeting,
    LogoImitiation,
    NoBranding,
    URLOrAttachment,
    RequestInfo,
    Urgency,
    PosesAs
}



//-----------Below used by YarnspinnerManager and personManager for animations in cutscenes--------------
/// <summary>
/// The different NPCs in the game
/// </summary>
public enum PersonsEnum
{
    Gwen,
    Bearmun,
    Hilaire,
    Cressida,
    Berg,
    Thisle,
    Meredith,
    Arthur
}

/// <summary>
/// The different moods that a NPC could have in a cutscene
/// </summary>
public enum MoodEnum
{
    Neutral,
    Happy,
    Angry
}
