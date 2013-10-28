class Bitmap
	# 直线： ax + by + c = 0
	# 起点与终点： x1,x2
	# 宽度：d
	# 颜色：color
	def draw_line_base(a,b,c,x1,x2,d,color)
		# 约定： |k| < 1
		t = Math.sqrt(a * a + b * b)
		wy = d / t * y
		dy = (wy / 2).ceil
		for x in x1..x2
			y = ((-c - a * x) / b).round
			for j in (y-dy)..(y+dy)
				dis = distance(a,b,c,x+0.5,y+0.5)
				per = [[1.0,0.5 * wy - dis + 0.5].min,0].max
				color.alpha = per * 255
				set_pixel(x,y,color)
			end
		end
	end
	def distance(a,b,c,x,y)
		return Math.abs(a*x+b*y+c)/Math.sqrt(a*a+b*b)
	end
end
