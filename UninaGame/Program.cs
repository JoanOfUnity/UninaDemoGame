// See https://aka.ms/new-console-template for more information
using UninaGame;

class Program
{
    static void Main(string[] args)
    {
        using (var game = new Game(1700, 900))
        {
            game.Run();
        }
    }
}
//vec4(aPosition, 1.0) * model * view * projection;