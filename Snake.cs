using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{

    delegate string ReadLineDelegate();
    internal class Snake
    {

        protected Random random = new Random();

        protected int xDimension;
        protected int yDimension;
        protected List<int> xPosition = new List<int>();
        protected List<int> yPosition = new List<int>();
        protected int snakeLenght;
        protected bool onlyGrown;
        protected int timegrown;

        protected string[] foodType;
        protected int foodCount;
        protected List<int> foodXPosition = new List<int>();
        protected List<int> foodYPosition= new List<int>();
        protected List<int> foodList = new List<int>();
        protected List<int> foodValue = new List<int>();
        //static int[] foodChar = new int[] {
        protected int grownYPosition;
        protected int grownXPosition;
        protected char direction; // w- up / d - right / s - down / a - left
        protected bool dead = false;
      

        public Snake()
        {
            this.xDimension = 10;
            this.yDimension = 20;
            this.direction = 'w';
            this.yPosition.Add( random.Next(1,yDimension - 1));
            this.xPosition.Add( random.Next(1,xDimension - 1));
            
            this.foodCount = random.Next(1, 3);
            //this.foodYPosition.Add( random.Next(1,yDimension - 1));
            //this.foodXPosition.Add( random.Next(1,xDimension - 1));
            this.foodType = new string[] { AsciiConverter(6), AsciiConverter(5), AsciiConverter(4), AsciiConverter(3) };
            this.snakeLenght = 1;
            this.onlyGrown = false;
            this.timegrown = 0;
        }

        protected void OneRound()
        {
            Move(this.direction);
            
            FoodCatchChecker();
            Drawer();
            

            Console.WriteLine("y pos: " + this.yPosition[0] + "  x Pos: " + this.xPosition[0]);
            Console.WriteLine("snake Lenght:  " + this.snakeLenght);
            DeathChecker();
        }

        public void StartGame()
        {
            FoodPlacerManager();
            Drawer();
            Console.WriteLine();
            Console.WriteLine("Press any key to start");
            Console.WriteLine("Press 9 to quit");
            Console.Read();
            Game();
        }

        protected void Game()
        {
            if (dead)
            {
                return;
            }

            while (!dead)
            {
                Input(500);
                OneRound();
            }

            
        }
        protected void Input(int speed) {
            System.Threading.Thread.Sleep(speed);
            if (Console.KeyAvailable)
            {
                var input = Console.ReadKey();
                KeyDirectionChange(input.Key);
            }
        }
        protected virtual void MoveBodySegment(int n)
        {
            if (this.snakeLenght == 1)
            {
                return;
            }

            for (int i = 1; i < n; i++)
            {
                this.yPosition[n - i] = this.yPosition[n - i - 1];
                this.xPosition[n - i] = this.xPosition[n - i - 1];
            }
        }      // moves the snakes body execpt the head
        protected virtual void MoveNSegment(int n)
        {
            

            if (this.direction == 'w')
            {
                this.yPosition[n]--;
            }
            else if (this.direction == 'd')
            {
                this.xPosition[n]++;
            }
            else if (this.direction == 's')
            {
                this.yPosition[n]++;
            }
            else if (this.direction == 'a')
            {
                this.xPosition[n]--;
            }
        }   // moves the snakes n body part

        protected virtual void Move(char direction) // moves the snake to the direction
        {
            
            if (!this.onlyGrown)
            {
                MoveBodySegment(this.snakeLenght);
                MoveNSegment(0);
                return;
            }

            MoveBodySegment(this.snakeLenght-1);
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

        protected void KeyDirectionChange(ConsoleKey command)
        {
            //var command = Console.ReadKey(true).Key;
            

            if (command == ConsoleKey.W)
            {
                if (this.direction != 's')
                {
                    this.direction = 'w';
                }
            }
            else if (command == ConsoleKey.D)
            {
                if (this.direction != 'a')
                {
                    this.direction = 'd';
                }
            }
            else if ( command == ConsoleKey.S)
            {
                if (this.direction != 'w')
                {
                    this.direction = 's';
                }
            }
            else if  (command == ConsoleKey.A)
                {
                if (this.direction != 'd')
                {
                    this.direction = 'a';
                }
            }

            //Move(this.direction);
        }           //  set the irection
        // ================ Move ==================
        protected void Dead()
        {
            this.dead = true;
            Console.Clear();
            Console.WriteLine("================================================================");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t"+ "\t" + "You died!");
            Console.WriteLine();
            Console.WriteLine("\t" + "\t" + "Score: " + "\t" + this.snakeLenght);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("================================================================");
        }
        protected void DeathChecker()
        {
            if (this.xPosition[0] >= this.xDimension)
            {
                Dead();
                return; 
            }
            else if (this.yPosition[0] >= this.yDimension)
            {
                Dead();
                return;
            }
            else if(this.xPosition[0] <= 0)
            {
                Dead();
                return;
            }
            else if (this.yPosition[0] <= 0)
            {
                Dead();
                return;
            }

            for (int i = 1; i < this.snakeLenght; i++)
            {
                if (!IfGwownArea(this.yPosition[i], this.xPosition[i])   && IfSnakeHead(this.yPosition[i], this.xPosition[i]))
                {
                    Dead();
                    return;
                }
            }
        }   // checks if dead
        // ================= Food ====================
        protected virtual void SnakeGrow(int y, int x)
        {
            this.timegrown = this.snakeLenght;

            if (this.direction == 'w')
            {
                this.yPosition.Add(this.foodYPosition[y] );
                this.xPosition.Add(this.foodXPosition[x]);
            }
            else if (this.direction == 'd')
            {
                this.yPosition.Add(this.foodYPosition[y]);
                this.xPosition.Add(this.foodXPosition[x]);
            }
            else if(this.direction == 's')
            {
                this.yPosition.Add(this.foodYPosition[y]);
                this.xPosition.Add(this.foodXPosition[x]);
            }
            else if(direction == 'a')
            {
                this.yPosition.Add(this.foodYPosition[y]);
                this.xPosition.Add(this.foodXPosition[x]);
            }

            this.snakeLenght++;
            this.onlyGrown = true;
        }

        protected bool IfFood(int inputy, int inputx)
        {
            for (int i = 0; i < this.foodYPosition.Count; i++)
            {
                    if (this.foodYPosition[i] == inputy && this.foodXPosition[i] == inputx)
                    {
                        return true;
                    }
            }

            return false;
        }   // if the y x cordinate is food
        protected int GetFoodId(int input)
        {
            int i = 0;
            while(i < this.foodYPosition.Count && this.foodYPosition[i] != input)
            {
                i++;
            }
            return i;
        }  // gets the idx food type
        protected void FoodCatch(int n)
        {
            FoodPlacer(n - 1);
        }  // when food was cathed

        protected int GetFoodType(int inputY, int inputX)
        {
            for (int i = 0; i < this.foodYPosition.Count; i++)
            {
                if (this.foodYPosition[i] == inputY && this.foodXPosition[i] == inputX)
                {
                    return this.foodList[i];
                }
            }
            return (-1);
            //return "";
        } // return the int value of a food

        protected void FoodAdd()
        {
            this.foodYPosition.Add(random.Next(1, yDimension - 1));
            this.foodXPosition.Add(random.Next(1, xDimension - 1));
            this.foodList.Add(random.Next(1, 5));
        } // adds a food to the list
        protected void FoodPlacerManager()
        {
            if (this.foodYPosition.Count == 0)
            {
                for (int i = 0; i < this.foodCount; i++)
                {
                    FoodAdd();
                }
                return;
            }


            if (this.foodYPosition.Count < this.foodCount)
            {
                for (int i = 0; i < (this.foodCount - this.foodYPosition.Count); i++)
                {
                    FoodAdd();
                }
            }

            if (this.snakeLenght > 3 && this.snakeLenght % 4 == 0)
            {
                this.foodCount++;
            }
        }  // manages food placement

        protected void FoodPlacer(int n)
        {
            this.foodYPosition[n] = random.Next(1, yDimension - 1);
            this.foodXPosition[n] = random.Next(1, xDimension - 1);
            if (IfSnake(this.foodYPosition[n],foodXPosition[n]))
            {
                FoodPlacer(n);
            }
        }       // places food in the lists n idx random non snake title

        protected void FoodCatchChecker()
        {
            for (int i = 0; i < this.foodYPosition.Count; i++)
            {
                for (int j = 0; j < this.foodXPosition.Count; j++)
                {
                    if (IfSnakeHead(this.foodYPosition[i], this.foodXPosition[j]))
                    {
                        this.grownYPosition = this.foodYPosition[i];
                        this.grownXPosition = this.foodXPosition[j];
                        SnakeGrow(i, j);
                        FoodCatch(GetFoodId(i));
                        FoodPlacerManager();
                    }
                }
            }
        }   // checks if catched the food


        // ========= visual Section ==========
        protected bool IfSnake(int inputYPosition, int inputXPosition)
        {
            for (int i = 0; i < this.yPosition.Count; i++)
            {
                if (this.yPosition[i] == inputYPosition && this.xPosition[i] == inputXPosition)
                {
                    return true;
                }
            }
            return false;

        } // checks if snake is the title
        protected bool IfSnakeHead(int inputYPosition, int inputXPosition)
        {
            if (this.yPosition[0] == inputYPosition && this.xPosition[0] == inputXPosition)
            {
                return true;
            }

            return false;
        }
        protected bool IfGwownArea(int inputYPosition, int inputXPosition)
        {
            if (this.grownYPosition == inputYPosition && this.grownXPosition == inputXPosition)
            {
                return true;
            }

            return false;
        }
        public void Drawer()
        {
            Console.Clear();

            for (int i = 0; i < this.yDimension + 1; i++)
            {
                string line = "";

                for (int j = 0; j < this.xDimension + 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        line = line + "X";
                    }
                    else if(i == 0 && j == this.xDimension)
                    {
                        line = line + "X";
                    }

                    else if(i == this.yDimension && j == 0)
                    {
                        line = line + "X";
                    }

                    else if (i == this.yDimension && j == this.xDimension)
                    {
                        line = line + "X";
                    }

                    else if (i == 0 || i == this.yDimension)
                    {
                        line = line + "=";
                    }
                    else if (IfSnakeHead(i, j))
                    {
                        line = line + AsciiConverter(1);
                    }
                    else if (IfGwownArea(i, j))
                    {
                        line = line + "@";
                    }
                    else if(IfSnake(i, j))
                    {
                        line = line + (char)248;
                    }
                    else if (IfFood(i, j))
                    {
                        line = line + this.foodType[(GetFoodType(i,j))];
                    }
                    else if (j == this.xDimension || j == 0)
                    {
                        line = line + "||";
                    }
                    else
                    {
                        line = line + " ";
                    }
                }
                Console.WriteLine(line);
                
            }
            
            var windowWidth = this.xDimension + 10;
            var windoHeight = this.yDimension + 10;
            windowWidth = Console.WindowWidth;
            windoHeight = Console.WindowHeight;

            
        }       // draws the picture

        public string AsciiConverter(int n)
        {
            char c = (char)n;
            return c.ToString();
        }

        
    }
}
