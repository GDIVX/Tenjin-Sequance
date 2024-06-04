using UnityEngine;

namespace Queue.Helpers
{
    public static class Vector2Helper
    {
        public static float AngleBetween360(Vector2 from, Vector2 to)
        {
            var dot = from.x * to.x + from.y * to.y;
            var det = from.x * to.y - from.y * to.x;
            
            var angle = Mathf.Atan2(det, dot) * Mathf.Rad2Deg;
            
            if (angle < 0) 
                angle += 360;

            return angle;   
        }
        // dot = x1*x2 + y1*y2      # dot product
        // det = x1*y2 - y1*x2      # determinant
        // angle = atan2(det, dot)  # atan2(y, x) or atan2(sin, cos)
    }
}