package com.vatscy.tetris;

import java.util.Random;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Point;
import android.graphics.Rect;
import android.view.Display;
import android.view.View;
import android.view.WindowManager;

public class TetrisView extends View {

	private static final int		INVALIDATE_SPAN	= 1000;
	private static final int		BLOCK_LENGTH	= 50;
	private static final int		DISPLAY_BLOCK_HEIGHT	= 13;
	private static final int		DISPLAY_BLOCK_WIDTH	= 10;
	private static final Block		BLOCK_Z			= new Block(new Point[][] {
			{ new Point(-1, 0), new Point(0, 0), new Point(0, 1),
			new Point(1, 1) },
			{ new Point(0, -1), new Point(0, 0), new Point(-1, 0),
			new Point(-1, 1) }						}, 1);
	private static final Block		BLOCK_L			= new Block(new Point[][] {
			{ new Point(-1, 0), new Point(0, 0), new Point(1, 0),
			new Point(1, -1) },
			{ new Point(-1, -1), new Point(0, -1), new Point(0, 0),
			new Point(0, 1) },
			{ new Point(-1, 1), new Point(-1, 0), new Point(0, 0),
			new Point(1, 0) },
			{ new Point(0, -1), new Point(0, 0), new Point(0, 1),
			new Point(1, 1) }						}, 2);
	private static final Block		BLOCK_O			= new Block(
															new Point[][] { {
			new Point(0, -1), new Point(0, 0), new Point(1, 0),
			new Point(1, -1)								} }, 3);
	private static final Block		BLOCK_S			= new Block(new Point[][] {
			{ new Point(1, 0), new Point(0, 0), new Point(0, 1),
			new Point(-1, 1) },
			{ new Point(0, -1), new Point(0, 0), new Point(1, 0),
			new Point(1, 1) }						}, 4);
	private static final Block		BLOCK_I			= new Block(new Point[][] {
			{ new Point(-1, 0), new Point(0, 0), new Point(1, 0),
			new Point(2, 0) },
			{ new Point(0, -1), new Point(0, 0), new Point(0, 1),
			new Point(0, 2) },
			{ new Point(-2, 0), new Point(-1, 0), new Point(0, 0),
			new Point(1, 0) },
			{ new Point(0, -2), new Point(0, -1), new Point(0, 0),
			new Point(0, 1) }						}, 5);
	private static final Block		BLOCK_R			= new Block(new Point[][] {
			{ new Point(0, -1), new Point(0, 0), new Point(0, 1),
			new Point(-1, 1) },
			{ new Point(-1, -1), new Point(-1, 0), new Point(0, 0),
			new Point(1, 0) },
			{ new Point(1, -1), new Point(0, -1), new Point(0, 0),
			new Point(0, 1) },
			{ new Point(-1, 0), new Point(0, 0), new Point(1, 0),
			new Point(1, 1) }						}, 6);
	private static final Block		BLOCK_T			= new Block(new Point[][] {
			{ new Point(-1, 0), new Point(0, 0), new Point(0, -1),
			new Point(1, 0) },
			{ new Point(0, -1), new Point(0, 0), new Point(1, 0),
			new Point(0, 1) },
			{ new Point(-1, 0), new Point(0, 0), new Point(0, 1),
			new Point(1, 0) },
			{ new Point(-1, 0), new Point(0, 0), new Point(0, -1),
			new Point(0, 1) }						}, 7);
	private static final Block[]	BLOCKS			= { BLOCK_Z, BLOCK_L,
			BLOCK_O, BLOCK_S, BLOCK_I, BLOCK_R, BLOCK_T };

	private final int				mDisplayWidth;
	private final Random			mRandom			= new Random();
	private Rect[]					srcRects		= new Rect[BLOCKS.length + 1];
	private Rect[][]				destRects		= new Rect[DISPLAY_BLOCK_HEIGHT][DISPLAY_BLOCK_WIDTH];
	private int[][]					screen			= new int[DISPLAY_BLOCK_HEIGHT][DISPLAY_BLOCK_WIDTH];
	private Point					position		= null;
	private Block					block			= null;
	private int						rot				= 0;

	public TetrisView(Context context) {
		super(context);
		WindowManager wm = (WindowManager) context
				.getSystemService(Context.WINDOW_SERVICE);
		Display disp = wm.getDefaultDisplay();
		Point size = new Point();
		disp.getSize(size);
		mDisplayWidth = size.x / DISPLAY_BLOCK_WIDTH;
	}

	@SuppressLint("DrawAllocation")
	@Override
	protected void onDraw(Canvas canvas) {
		Bitmap blockImage = BitmapFactory.decodeResource(getResources(),
				R.drawable.tetris);

		for (int i = 0; i < BLOCKS.length + 1; i++) {
			srcRects[i] = new Rect(i * BLOCK_LENGTH, 0, (i + 1) * BLOCK_LENGTH,
					BLOCK_LENGTH);
		}

		for (int i = 0; i < DISPLAY_BLOCK_HEIGHT; i++) {
			for (int j = 0; j < DISPLAY_BLOCK_WIDTH; j++) {
				destRects[i][j] = new Rect(j * mDisplayWidth,
						i * mDisplayWidth, (j + 1) * mDisplayWidth, (i + 1)
								* mDisplayWidth);
			}
		}

		for (int i = 0; i < DISPLAY_BLOCK_HEIGHT; i++) {
			for (int j = 0; j < DISPLAY_BLOCK_WIDTH; j++) {
				screen[i][j] = 0;
			}
		}

		putStartPos();

		for (int i = 0; i < block.getSq()[0].length; i++) {
			screen[position.y + block.getSq()[rot][i].y][position.x
					+ block.getSq()[rot][i].x] = block.getColor();
		}

		for (int i = 0; i < DISPLAY_BLOCK_HEIGHT; i++) {
			for (int j = 0; j < DISPLAY_BLOCK_WIDTH; j++) {
				canvas.drawBitmap(blockImage, srcRects[screen[i][j]],
						destRects[i][j], null);
			}
		}

		try {
			Thread.sleep(INVALIDATE_SPAN);
			invalidate();
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	private void putStartPos() {
		position = new Point(DISPLAY_BLOCK_WIDTH / 2, 5);
		block = BLOCKS[mRandom.nextInt(BLOCKS.length)];
		rot = mRandom.nextInt(block.getSq().length);
	}
}
