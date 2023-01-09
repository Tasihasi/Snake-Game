using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    internal class SnakeImprovedFood : Snake
    {
        protected int[] grownArea = new int[2];
        protected List<int> foodValue = new List<int>();

        public SnakeImprovedFood():base() { 
        }

        protected override void MoveBodySegment(int n)
        {
            if (this.snakeLenght == 1)
            {
                return;
            }
            if (!this.onlyGrown)
            {
                for (int i = 1; i < n; i++)
                {
                    this.yPosition[n - i] = this.yPosition[n - i - 1];
                    this.xPosition[n - i] = this.xPosition[n - i - 1];
                }
                return;
            }
            
            
        }

        protected override void Move(char direction)
        {
            if (!this.onlyGrown)
            {
                MoveBodySegment(this.snakeLenght);
                MoveNSegment(0);
                return;
            }

            MoveBodySegment(this.snakeLenght - 1);
            MoveNSegment(0);
            this.timegrown--;
            if (this.timegrown == 0)
            {
                this.onlyGrown = false;
                this.grownYPosition = -1;
                this.grownXPosition = -1;
            }

            //MoveBodySegment(2);

            Console.WriteLine("moved");
        }


        protected override void SnakeGrow(int y, int x)
        {
            if (GetFoodType(y, x) == 1)
            {
                this.timegrown = this.snakeLenght;

                if (this.direction == 'w')
                {
                    this.yPosition.Add(this.foodYPosition[y]);
                    this.xPosition.Add(this.foodXPosition[x]);
                }
                else if (this.direction == 'd')
                {
                    this.yPosition.Add(this.foodYPosition[y]);
                    this.xPosition.Add(this.foodXPosition[x]);
                }
                else if (this.direction == 's')
                {
                    this.yPosition.Add(this.foodYPosition[y]);
                    this.xPosition.Add(this.foodXPosition[x]);
                }
                else if (direction == 'a')
                {
                    this.yPosition.Add(this.foodYPosition[y]);
                    this.xPosition.Add(this.foodXPosition[x]);
                }

                this.snakeLenght++;
                this.onlyGrown = true;
                return;
            }


        }
        protected void SnakeIsGrowing()
        {

        }

        protected void SnakeHasGrown()
        {
            this.grownArea = new int[2];
        }
    }
}
