#encoding:utf-8
# Arce Script : Actor - xp.rb
# Describe the Actor DataEditor Page

require "./Help.rb"
require "./File - xp.rb"

singleRun = Help.Check_Window("编辑角色 - RPG Maker XP" , "actor" , "角色")

Builder.In(Builder.Add(:Group))
	Builder.Order(2)
	Builder.Add(:text, { actual: :name, text: "名称" })
	Builder.Add(:choose, {actual: :class_id, text: "职业", choice: { nil => FileChoice.new(Data["classes"]) } } )
	Builder.Next()

	Builder.Add(:int, {actual: :initial_level, text: "初期等级"})
	Builder.Add(:int, {actual: :fianl_level, text: "最终等级"})
	Builder.Next()

	Builder.Add(:exp, {actual: [:exp_basis,:exp_inflation], text: "exp 曲线"})
	Builder.Next()

	Builder.Add(:image, {actual: [:character_name,:character_hue], text: "角色脸谱"})
	Builder.Next()

	Builder.Add(:image, {actual: [:battle_name, :battle_hue], text: "战斗图"})
	Builder.Next()
	Builder.Order(0)
	Builder.Next()
	Builder.Order(0)

	Builer.Add(:actor_parameter,{actual: :parameter, text: "能力值"}) do |actor|
		actor.Add("MaxHP")
		actor.Add("MaxSP")
		actor.Add()
		actor.Add("力量")
		actor.Add("灵巧")
		actor.Add()
		actor.Add("速度")
		actor.Add("魔力")
	end

	Builder.In(Builder.Add("Group"))
		choice = FileChoice.new(Data["class"],:id) do |target,parent|
			target in Data["class"][parent["class"]].
		end
		Builder.Add(:choose, { actual: :blabla, label: 1 choice: { 0 => "无", nil => choice ]) }, text: Help.GetDefaultText() } )

	Builder.Out
Builder.Out
(1..3).each{ Builder.Out } if singleRun