using OpenRGB.NET;

namespace SeirenEmote;

class Program
{
    static void Main(string[] args)
    {
        byte[][] jaggedArray =
        {
            // 1st row
            new byte[3] {0, 0, 0},
            new byte[3] {0, 0, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {0, 0, 0},
            new byte[3] {0, 0, 0},
            // 2nd row
            new byte[3] {0, 0, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {0, 0, 0},
            // 3rd row
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {0, 0, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {0, 0, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            // 4th row
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            // 5th row
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {0, 0, 0},
            new byte[3] {0, 0, 0},
            new byte[3] {0, 0, 0},
            new byte[3] {0, 0, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            // 6th row
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 0, 0},
            new byte[3] {255, 0, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            // 7th row
            new byte[3] {0, 0, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {0, 0, 0},
            // 8th row
            new byte[3] {0, 0, 0},
            new byte[3] {0, 0, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {255, 255, 0},
            new byte[3] {0, 0, 0},
            new byte[3] {0, 0, 0},
        };


        try
        {
            using var client = new OpenRgbClient();
            client.Connect();

            var devices = client.GetAllControllerData();
            var seirenEmote = devices.First(device => device.Name == "Razer Seiren Emote");
            if (seirenEmote == null)
            {
                Console.WriteLine("Razer Seiren Emote is not connected.");
                return;
            }

            var colors = new Color[seirenEmote.Colors.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = new Color(jaggedArray[i][0], jaggedArray[i][1], jaggedArray[i][2]);
            }
            client.UpdateLeds(seirenEmote.Index, colors);
        }
        catch (Exception e)
        {
            if (e is System.Net.Sockets.SocketException)
            {
                Console.WriteLine("OpenRGB server is not running.");
                return;
            }

            Console.WriteLine(e.Message);
            return;
        }
    }
}
