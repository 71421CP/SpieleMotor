using System.Collections.Generic;

using Math = System.Math;
using ConsoleColor = System.ConsoleColor;

namespace SpieleMotor.Entities
{
    class Ghost : AEntity
    {
        private int m_moveCounter;

        public Ghost(Vector2 _position) : base(_position, ConsoleColor.White, '▒')
        {

        }

        public override void Update()
        {
            m_moveCounter = (m_moveCounter + 1) % 2;

            if (m_moveCounter == 0)
            {
                int xDelta = Game.Get.MyPlayer.m_Position.m_XPos - m_Position.m_XPos;
                int yDelta = Game.Get.MyPlayer.m_Position.m_YPos - m_Position.m_YPos;
                float distance = (float)Math.Sqrt(xDelta * xDelta + yDelta * yDelta);
                if (distance < 20)
                {
                    m_Position.m_XPos += Math.Sign(xDelta);
                    m_Position.m_YPos += Math.Sign(yDelta);
                    m_Color = ConsoleColor.Red;
                }
                else
                {
                    m_Position.m_XPos += Game.Get.m_rdm.Next(-1, 2);
                    m_Position.m_YPos += Game.Get.m_rdm.Next(-1, 2);
                    m_Color = ConsoleColor.White;
                }                

                List<AEntity> colls = Game.Get.CollisionWith(this);
                foreach (AEntity entity in colls)
                {
                    if (entity is Player)
                    {
                        entity.Destroy();
                    }
                }
            }

            base.Update();
        }        
    }
}
