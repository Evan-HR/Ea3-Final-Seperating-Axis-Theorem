using System;
using System.Collections.Generic;
using System.Collections;
using System.Numerics;
	
// 
	
public class Polygon
{
	private Point c; //the centre point of the polygon
	private Point[] pnts; // the points of the polygon

	private int n; // the number of points in the polygon
	
	public Polygon(List<Point> pointsList) 
	{
		pnts = pointsList.ToArray();
		double avgx = 0; 
		double avgy = 0;
		for (int i = 0; i < pnts.Length; i++)
		{
			avgx += pnts[i].get_x();
			avgy += pnts[i].get_y();
		}
		c = new Point(avgx / pnts.Length, avgy / pnts.Length);
		n = pnts.Length;
	}

	public Point get_c()
	{
		return c;
	}

	public int get_n()
	{
		return n;
	}

	public Point get_point(int index)
	{
		try 
		{ 
			return pnts[index]; 
		}
		catch(System.IndexOutOfRangeException ex)
    	{
        System.ArgumentException argEx = new System.ArgumentException("Index is out of range", "index", ex);
        throw argEx;
		}
	}
	
	public void translate(Vector delta_d)
	{
		for (int i = 0; i < pnts.Length; i++)
		{
			pnts[i].set_x(pnts[i].get_x() + delta_d.get_x());
			pnts[i].set_y(pnts[i].get_y() + delta_d.get_y());
		}
		
		c.set_x(c.get_x() + delta_d.get_x());
		c.set_y(c.get_y() + delta_d.get_y());
	}
	
	public void rotate(double delta_r) // pass arguments in radians!
	{
		for (int i = 0; i < pnts.Length; i++)
		{
			pnts[i] = pnts[i].rotate(c, delta_r);
		}
	}
	
	public Tuple<Vector, Vector> project(Vector v)
	{
	// given a vector, this finds the minimum and maximum projections of the shape's points when projected onto this vector
	// returns the shape's projection onto a given vector in the form of a tuple of two vectors.
		
		Vector[] projList = new Vector[pnts.Length];
		for (int i = 0; i < pnts.Length; i++)
		{
			projList[i] = pnts[i].makeVector().project(v);
		}	
		Vector Min = projList[0];
		Vector Max = projList[0];
		for (int i = 1; i < pnts.Length; i++)
		{
			Vector V_i = projList[i];
			
			if (V_i < Min) { Min = V_i; }
			if (V_i > Max) { Max = V_i; }
		}	
		return new Tuple<Vector, Vector> (Min, Max);
	}
	public Boolean collisionCheck(Polygon other)
	{
		//take all normal vectors from both polygons to project the polygons onto, 
		// then compare the min/max pairs of both polygons

		//I need that funky wrapping edge-loop where I make edges out of pairs of points.
		// since all shapes are closed, edge# = point#
		int N = n + other.get_n();
		Vector[] edges = new Vector[N];
		Vector e;
		for (int i = 0; i < N; i++)
		{
		// use if statement while iterating through the edge list to assure no edges are used that 
		// are not in either shape. 
			if (i < n) // in first shape
			{
				e = new Vector(pnts[(i + 1) % n], pnts[i]); // this achieves a wrapping edge 
				edges[i] = e.orthogonal();
			}
			else // in second shape
			{
				e = new Vector(other.get_point( ((i-n)+1) % other.get_n()), other.get_point(i-n));
				edges[i] = e.orthogonal();
			}
		}
		Tuple<Vector, Vector> projA; //tuples represent the total projections in the form (min, max)
		Tuple<Vector, Vector> projB;
		for (int i = 0; i < N; i++)
		{
			projA = this.project(edges[i]); 

			projB = other.project(edges[i]);
			// if max of A is less than min of B, or min of B is greater than max of A for any vector --> no collision.
			if (!(projA.Item2 >= projB.Item1 || projB.Item2 <= projA.Item1))
			{
				return false;
			}
		}
		return true;
	}


	static void Main(string[] args)
	{
		Point p1 = new Point(0.0,0.0);
		Point p2 = new Point(1.0,0.0);
		Point p3 = new Point(0.0,1.0);
		Point p4 = new Point(2.0,1.0);
		Point p5 = new Point(3.0,0.0);
		Point p6 = new Point(3.0,1.0);
		Point p7 = new Point(4.0,0.0);
		
		List<Point> P = new List<Point>();
		P.Add(p1); P.Add(p2); P.Add(p3);
		List<Point> Q = new List<Point>();
		Q.Add(p4); Q.Add(p5); Q.Add(p6); Q.Add(p7);
		Polygon P1 = new Polygon(P);
		Polygon P2 = new Polygon(Q);
		
		Console.WriteLine(P1.collisionCheck(P2));
	}
}
	