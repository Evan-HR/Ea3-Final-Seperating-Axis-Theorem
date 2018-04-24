using System;

public class Vector
{
	private double x;
	private double y;
	
	public Vector(double nx, double ny)
	{
		x = nx;
		y = ny;
	}

	public Vector(Point p1, Point p2)
	{
		x = p2.get_x() - p1.get_x();
		y = p2.get_y() - p1.get_y();
	}
	
	public double get_x()
	{
		return x;
	}
	
	public double get_y()
	{
		return y;
	}
	public void set_x(double nx)
	{
		x = nx;
	}
	
	public void set_y(double ny)
	{
		y = ny;
	}
	
	public double angle()
	{
		return Math.Atan2(y, x);
	}
	
	public double magnitude()
	{
		return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
	}
	
	public Vector multV(float c)
	{
		Vector v2 = new Vector(x*c, y*c);
		return v2;
	}
	
	public Vector addV(Vector v1, Vector v2)
	{
		Vector v3 = new Vector(v1.get_x() + v2.get_x(), v1.get_y() + v2.get_y());
		return v3;
	}
	
	public double[] arrV()
	{
		return new double[] {x, y};
	}
	
		public override int GetHashCode()
	{
		return (int) (x + y);
	}
	
	public override bool Equals(Object obj)
	{
		return obj is Vector && this == (Vector)obj;
	}
	
	public static bool operator >(Vector a, Vector b)
	{
		return a.magnitude() > b.magnitude();
	}
	
	public static bool operator >=(Vector a, Vector b)
	{
		return a.magnitude() >= b.magnitude();
	}
	
	public static bool operator ==(Vector a, Vector b)
	{
		return a.magnitude() == b.magnitude();
	}
	
	public static bool operator <=(Vector a, Vector b)
	{
		return a.magnitude() <= b.magnitude();
	}
	
	public static bool operator <(Vector a, Vector b)
	{
		return a.magnitude() < b.magnitude();
	}
	
	public static bool operator !=(Vector a, Vector b)
	{
		return !(a == b);
	}
	
	public double dot(Vector v1, Vector v2){
		return (v1.get_x() * v2.get_x() + v1.get_y() * v2.get_y());
	}

	public Vector orthogonal(){
		return new Vector(-y, x);
	}
	
	public Vector project(Vector other)
	{
		double c = dot(this, other) / Math.Sqrt(Math.Pow(other.get_x(),2) + Math.Pow(other.get_y(),2));
		
		return new Vector(c*other.get_x(), c*other.get_y());
	}
	public override string ToString(){
		return "(" + x + " , " + y + ")";
	}
	
	//public static int Main(string[] args)
	//{
	//	return 0;
	//}
}
