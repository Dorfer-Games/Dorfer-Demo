using System;
using System.Collections.Generic;

[Serializable]
public class GiftData
{
    public int eventId;
    public string eventType;
    public Present data;
}

[Serializable]
public class Present
{
    public int giftId;
    public int repeatCount;
    public string groupId;
    public string userId;
    public string secUid;
    public string uniqueId;
    public string nickname;
    public string profilePictureUrl;
    public int followRole;
    public List<UserBadge> userBadges;
    public UserDetails userDetails;
    public FollowInfo followInfo;
    public bool isModerator;
    public bool isNewGifter;
    public bool isSubscriber;
    public object topGifterRank;
    public string msgId;
    public string createTime;
    public string displayType;
    public string label;
    public bool repeatEnd;
    public Gift gift;
    public string describe;
    public int giftType;
    public int diamondCount;
    public string giftName;
    public string giftPictureUrl;
    public long timestamp;
    public string receiverUserId;
}

[Serializable]
public class Gift
{
    public int gift_id;
    public int repeat_count;
    public int repeat_end;
    public int gift_type;
}