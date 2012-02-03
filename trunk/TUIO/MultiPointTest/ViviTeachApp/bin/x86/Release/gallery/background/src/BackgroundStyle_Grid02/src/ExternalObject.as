package 
{
	import flash.display.Graphics;
	import flash.display.Shape;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.geom.Rectangle;
	
	/**
	 * ...
	 * @author david
	 */
	public class ExternalObject extends Sprite 
	{
		
		public function ExternalObject():void 
		{
			var shape:Shape = new Shape();
			shape.name = "External";
			this.addChild(shape);
			
			
			//Paint(this.graphics, new Rectangle(0,0,1280,700));
		}
		
	
		
		public function Paint(g:Graphics,bound:Rectangle):void
		{
			g.clear();
			g.beginFill(0xcbcbcb, 1);
			g.drawRect(bound.x, bound.y, bound.width, bound.height);
			g.endFill();

			var hh:int = bound.height / 4;
			var space:int = (bound.height-(hh*3))/4;
			var ww:int = bound.width - space;
	
			var rect:Rectangle = new Rectangle((bound.width - ww) / 2, space, ww, hh);
			DrawGrid(g, rect);
			
			rect.y += space+rect.height;
			DrawGrid(g,rect);
			
			rect.y += space+rect.height;
			DrawGrid(g,rect);
		}
		
		public function DrawGrid(g:Graphics,rect:Rectangle) 
		{
			var hh:int = rect.height / 3;
			
		
			g.lineStyle(0, 0x000000,0);
			g.beginFill(0xffffff, 1);
			g.drawRect(rect.left,rect.top,rect.width, rect.height);
			g.endFill();
			
			//外框
			g.lineStyle(2, 0x000000, 1);
			g.moveTo(rect.left,rect.top);
			g.lineTo(rect.right, rect.top);
			g.moveTo(rect.left,rect.bottom)
			g.lineTo(rect.right, rect.bottom);
			
			//黑線
			g.lineStyle(2, 0x000000, 1);
			g.moveTo(rect.left, rect.top+(hh*1));
			g.lineTo(rect.right, rect.top+(hh*1));
			
			
			
			//綠線
			g.lineStyle(2, 0x33ffff, 1);
			g.moveTo(rect.left, rect.top+(hh*2));
			g.lineTo(rect.right, rect.top+(hh*2));
			
		
			
		}
		
		
	}
	
}