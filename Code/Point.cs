using System;

public class Point
{
	private double x;
	private double y;
	
	public Point(double nx, double ny)
	{
		x = nx;
		y = ny;
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
	
	public void translate(double dx, double dy)
	{
		x += dx;
		y += dy;
	}

	public Point rotate(Point other, double theta)
	{
		double nx = Math.Cos(theta) * (x - other.get_x()) - Math.Sin(theta) * (y - other.get_y()) + other.x;
		double ny = Math.Sin(theta) * (y - other.get_y()) + Math.Cos(theta) * (x - other.get_x()) + other.y;
		return new Point(nx, ny);
	}

	public Vector makeVector()
	{
		return new Vector(get_x(), get_y());
	}
}