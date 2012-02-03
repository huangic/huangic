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
		}
		
	
		
		public function Paint(g:Graphics,bound:Rectangle):void
		{
			g.clear();
			g.lineStyle(1, 0xb5b5b5);
			g.beginFill(0xffffff, 1);
			g.drawRect(bound.x, bound.y, bound.width, bound.height);
			g.endFill();
			
			for (var yy:int = 0; yy < 100; yy++)
			{
				//ten steps across
				var gridtop:int = yy * 50;
				if (gridtop >= bound.height)
					break;
				for (var xx:int = 0; xx < 100; xx++)
				{
					var gridleft:int = xx * 50;
					if (gridleft >= bound.width)
						break;

					g.drawRect(xx * 50, yy * 50, 50, 50);

				}
			}
			
		}
		
	}
	
}