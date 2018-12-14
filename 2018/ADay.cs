namespace AdventOfCode._2018
{
    public abstract class ADay
    {
        public void execute(int taskId)
        {
            switch (taskId)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
            }
        }

        protected abstract void Task1();

        protected abstract void Task2();
    }
}