using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kuhpik
{
    [Serializable]
    public class TTSDKEvents
    {
        public event Action<LikeData> Liked;
        public event Action<GiftData> Gifted;
        public event Action<CommentData> Commented;

        private Queue<LikeData> _likes = new Queue<LikeData>();
        private Queue<GiftData> _gifts = new Queue<GiftData>();
        private Queue<CommentData> _comments = new Queue<CommentData>();

        public void TryRaiseEvents()
        {
            if (_likes.TryDequeue(out var like))
            {
                Liked?.Invoke(like);
            }
            if (_gifts.TryDequeue(out var gift))
            {
                Gifted?.Invoke(gift);
            }
            if (_comments.TryDequeue(out var comment))
            {
                Commented?.Invoke(comment);
            }
        }

        public void AddLikeEvent(LikeData likeData)
        {
            Debug.Log("<color=black>Like: </color>" + likeData.data.nickname);
            _likes.Enqueue(likeData);
        }

        public void AddGiftEvent(GiftData giftData)
        {
            Debug.Log("<color=green>Gift: </color>" + giftData.data.nickname);
            _gifts.Enqueue(giftData);
        }

        public void AddCommentEvent(CommentData commentData)
        {
            Debug.Log("<color=blue>Comment: </color>" + commentData.data.comment);
            _comments.Enqueue(commentData);
        }

        public void Clear()
        {
            _gifts.Clear();
            _comments.Clear();
            _likes.Clear();
        }
    }
}