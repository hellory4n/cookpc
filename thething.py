string = ""
color = 0
x = 0
y = 0
while color != 256:
	string = string + f"paint {x} {y} {color}\n"
	x = x + 1
	color = color + 1
	if x == 16:
		x = 0
		y = y + 1
print(string)
