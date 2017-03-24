package com.vatscy.tetris;

import android.graphics.Point;

public class Block {

	private Point[][]	sq;
	private int			color;

	public Block(Point[][] sq, int color) {
		this.sq = sq;
		this.color = color;
	}

	public Point[][] getSq() {
		return sq;
	}

	public void setSq(Point[][] sq) {
		this.sq = sq;
	}

	public int getColor() {
		return color;
	}

	public void setColor(int color) {
		this.color = color;
	}

}
