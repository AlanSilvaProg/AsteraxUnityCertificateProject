using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtendedClasse
{
    public static class ExtendedTransform
    {
        /// <summary>
        /// Check if the current transform is out of screen and if yes wrap the transform back to the screen in the opposite side
        /// </summary>
        /// <param name="transform"></param>
        public static void CheckLimits(this Transform transform, Action callbackWrapped = null)
        {
            CheckHorizontal();
            CheckVertical();

            void CheckVertical()
            {
                var worldPosition = transform.position;
                worldPosition.y -= transform.localScale.y / 2;
                var screenFixed = Camera.main.WorldToScreenPoint(worldPosition);

                if (screenFixed.y > Screen.height)
                {
                    var newPosition = Camera.main.WorldToScreenPoint(transform.position);
                    newPosition.y = 0;
                    worldPosition = Camera.main.ScreenToWorldPoint(newPosition);
                    callbackWrapped?.Invoke();
                    callbackWrapped = null;
                }
                else
                {
                    worldPosition = transform.position;
                    worldPosition.y += transform.localScale.y / 2;
                    screenFixed = Camera.main.WorldToScreenPoint(worldPosition);
                    if (screenFixed.y < 0)
                    {
                        var newPosition = Camera.main.WorldToScreenPoint(worldPosition);
                        newPosition.y = Screen.height;
                        worldPosition = Camera.main.ScreenToWorldPoint(newPosition);
                        callbackWrapped?.Invoke();
                        callbackWrapped = null;
                    }
                    else
                    {
                        worldPosition = transform.position;
                    }
                }

                transform.position = worldPosition;
            }
            
            void CheckHorizontal()
            {
                var worldPosition = transform.position;
                worldPosition.x -= transform.localScale.x / 2;
                var screenFixed = Camera.main.WorldToScreenPoint(worldPosition);

                if (screenFixed.x > Screen.width)
                {
                    var newPosition = Camera.main.WorldToScreenPoint(transform.position);
                    newPosition.x = 0;
                    worldPosition = Camera.main.ScreenToWorldPoint(newPosition);
                    callbackWrapped?.Invoke();
                    callbackWrapped = null;
                }
                else
                {
                    worldPosition = transform.position;
                    worldPosition.x += transform.localScale.x / 2;
                    screenFixed = Camera.main.WorldToScreenPoint(worldPosition);
                    if (screenFixed.x < 0)
                    {
                        var newPosition = Camera.main.WorldToScreenPoint(worldPosition);
                        newPosition.x = Screen.width;
                        worldPosition = Camera.main.ScreenToWorldPoint(newPosition);
                        callbackWrapped?.Invoke();
                        callbackWrapped = null;
                    }
                    else
                    {
                        worldPosition = transform.position;
                    }
                }

                transform.position = worldPosition;
            }
        }

        public static bool OutOfBounds(this Transform transform)
        {
            var worldPosition = transform.position;
            var screenFixed = Camera.main.WorldToScreenPoint(worldPosition);
            return screenFixed.x < 0 || screenFixed.x > Screen.width || screenFixed.y > Screen.height || screenFixed.y < 0;
        }
    }
}