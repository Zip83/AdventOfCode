namespace AdventOfCode._2018
{
    public abstract class ADay
    {
        private readonly bool test;
        
        protected abstract string FileName
        {
            get;
        }
        
        protected abstract string FileNameExample
        {
            get;
        }
        
        protected abstract void Task1(string fileName);

        protected abstract void Task2(string fileName);
        
        public ADay(bool test)
        {
            this.test = test;
        }
        
        public void execute(int taskId)
        {
            switch (taskId)
            {
                case 1:
                    Task1(getInputPath());
                    break;
                case 2:
                    Task2(getInputPath());
                    break;
            }
        }

        private string getInputPath()
        {
            return test ? FileNameExample : FileName;
        }
    }
}