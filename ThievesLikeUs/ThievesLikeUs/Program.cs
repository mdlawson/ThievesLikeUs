using System;

namespace ThievesLikeUs {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Thieves game = new Thieves())
            {
                game.Run();
            }
        }
    }
#endif
}

