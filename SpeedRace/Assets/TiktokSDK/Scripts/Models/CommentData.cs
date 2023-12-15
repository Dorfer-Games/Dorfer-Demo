using System;
using System.Collections.Generic;

[Serializable]
public class CommentData
{
    public int eventId;
    public string eventType;
    public Comment data;
}

[Serializable]
public class Comment
{
    public string comment;
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
}
