using System.Linq;

namespace SkipList
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var mySkipList = new SkipList<int>(Enumerable.Range(1,5).ToList());
        }
    }
}
