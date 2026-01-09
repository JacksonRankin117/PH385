using System;

class Program {
    static void Main(String[] args) {
        Vec3 a = new Vec3(1, 2, 3);
        Vec3 b = new Vec3(4, 5, 6);

        Vec3 result = Vec3.Cross(a, b);

        Console.WriteLine(result.ToString(6));
    }
}