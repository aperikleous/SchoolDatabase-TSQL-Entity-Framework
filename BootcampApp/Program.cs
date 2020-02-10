using ServicesLibrary;

namespace BootcampApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Services s = new Services();
            s.Start();
        }
    }
}
