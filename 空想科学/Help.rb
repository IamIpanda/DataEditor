#encoding:UTF-8
# Arce Script : Help.rb
# All the Methods Needed in Running

class Help
	def self.Check_Window(title)
		return false if Window.Exists
		Window.Create
		Window.Title = title
		Builder.In(Window)
		true
	end
	def self.Check_Window_And_Tab(title,dataname,name)
		if self.Check_Window(title)
			Builder.In(Builder.Add("Tabs"))
			Builder.In(Builder.Add("Tab",{actual: dataname, text: name}))
			Builder.In(Builder.Add("List",id: :id))
			return true
		end
		false
	end
	def self.Auto_Get_Text(*args)
		watch = []
		id = args[0]["id"]
		name = args[0]["name"]
		watch.push id
		watch.push name
		return [sprintf("0:d3",id) + ":" + name,watch]
	end
	def self.Get_Default_Text
		return Text.new do |*args|
			Help.Auto_Get_Text(*args)
		end
	end
	def self.VX_Image_Split
		
	end
end

Color = Struct.new(:red,:green,:blue,:alpha)
Rect = Struct.new(:x,:y,:width,:height)
Tone = Struct.new(:red,:green,:blue,:gray)
Text = Struct.new(:proc,:watch) do 
	def ToString(*args)
		ans = self.proc.call(*args)
		self.watch = ans[1]
		return ans[0]
	end
	def initialize(&block)
		self.proc = block
	end
end
FileChoice = Struct.new(:data,:id,:filter,:watch) do
	def initialize(data,id = :id,&filter)
		self.data = data
		self.id = id
		self.filter = filter
		self.watch = []
	end
end


	choice = {0=> "（无）",1=> "敌方单体",2=> "敌方全体",3=> "己方单体",4=> "己方全体",5=> "己方单体（HP 0）",6=> "己方全体（HP 0）",7=> "使用者"}
	hash = {actual: :scope , text: "效果范围", choice: choice} 
	puts hash



