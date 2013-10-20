# Arce Script : main.rb
# Describe the main logic of the Arce.

# Welcome
# Same time , initialize all the modules.

choice = Choose.Show("欢迎使用Arce。您想要做什么？",["新建数据文件","打开数据文件","执行配置文件"])
case choice
when 0
	MessageBox.Show("暂未提供此功能！")
when 1
	file = Window.OpenFile
when 2

end

# TODO : continue to think.