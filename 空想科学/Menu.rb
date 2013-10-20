# Arce Script : Menu.rb
# Add and run all the Menu items .

class MenuCommand
end


Window.Menu.Clear()
Window.Menu.Add("文件".true)
Window.Menu.Add("新建文件",false ,Proc.new {} , Proc.new { false } )
Window.Menu.Add("打开文件",false ,Proc.new { MenuCommand.OpenFile() } , Proc.new { true })



# TODO：Continue to think.