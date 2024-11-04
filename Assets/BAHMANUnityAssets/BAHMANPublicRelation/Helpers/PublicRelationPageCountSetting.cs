using System;
[Serializable]
public class PublicRelationPageCountSetting 
{
    public GameScenes SceneName;
    public PublicRelationType RelationType;
    public int SceneCount;
}

public enum PublicRelationType { Share, Rate, OtherProduct, Donate, Message }

