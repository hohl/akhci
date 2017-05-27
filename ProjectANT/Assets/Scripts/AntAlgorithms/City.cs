﻿/****************************************************
 * IML ACO implementation for TSP 
 * More information: http://hci-kdd.org/project/iml/
 * Author: Andrej Mueller
 * Year: 2017
 *****************************************************/

/* City represents a city in the TSP */

public class City
{
    private int id;
    private int xPosition;
    private int yPosition;

    public City(int xPosition, int yPosition, int id)
    {
        this.id = id;
        this.xPosition = xPosition;
        this.yPosition = yPosition;
    }

    public int getId()
    {
        return id;
    }

    public int getXPosition()
    {
        return xPosition;
    }

    public void setXPosition(int xPosition)
    {
        this.xPosition = xPosition;
    }

    public int getYPosition()
    {
        return yPosition;
    }

    public void setYPosition(int yPosition)
    {
        this.yPosition = yPosition;
    }
}