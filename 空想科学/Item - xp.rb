#encoding:UTF-8
# Arce Script : Item - xp.rb
# Describe the Item DataEditor Page
	
require "./Help.rb"

Builder.In(Builder.add(:group))
	hash = {:actual => :name, :text =>"名称"}
	Builder.Add(:text,{:actual => :name, :text => "名称"})
	Builder.Add(:image, {:actual => :icon_name , :text => "图标" , :path => "\\Icons"})
	Builder.Next()

	Builder.Add(:text, {:actual => :description , :text => "说明" })
	Builder.Next()

	choice = {0=> "（无）",1=> "敌方单体",2=> "敌方全体",3=> "己方单体",4=> "己方全体",5=> "己方单体（HP 0）",6=> "己方全体（HP 0）",7=> "使用者"}
	Builder.Add(:choose, {:actual => :scope , :text => "效果范围", :choice => choice})
	Builder.Add(:choose, {:actual => :occasion, :text => "可能使用时", :choice => {0 => "平时", 1 => "战斗中", 2 => "菜单中", 3 => "不能使用"}})
	Builder.Next()

	Builder.Add(:choose, {:actual => :animation1_id, :text => "使用方的动画", :choice => {  0 => "无" , -10 => Filechoice.new("animation")} })
	Builder.Add(:choose, {:actual => :animation2_id, :text => "对象方的动画", :choice => {  0 => "无" , -10 => Filechoice.new("animation")} })
	Builder.Next()

	Builder.Add(:audio, {:actual => :menu_se, :text => "菜单画面使用的SE",  :type => :SE})
	Builder.Add(:choose, {:actual => :choose, :text => "公共事件", :choice => { 0 => "无" , -10 => Filechoice.new("commonevent") } })
	Builder.Next()

	Builder.Add(:int , {:actual => :proce , :text => "价格"})
	Builder.Add(:boolchoose , {:actual => :consumable , :text => "消耗" ,:true => "消耗", :false => "不消耗"})
	choice = {0 => "无", 1=> "MaxHP", 2=> "MaxSP",3=>"",4=>"",5=>"",6=>""}
Builder.Out		