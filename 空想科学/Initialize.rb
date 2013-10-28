# Arce Sctipt : Initialize.rb
# Initialize all the Constant Ruby need from C#

=begin
# That's for test alone.
module DataEditor
	module Ruby
		module RubyBuilder
			def self.Instance
				return {}
			end
		end
		module RubyLinker
			def self.Instance 
				return {}
			end
		end
	end
end
=end

Builder = DataEditor::Ruby::RubyBuilder.Instance
Link    = DataEditor::Ruby::RubyLinker.Instance
Engine  = DataEditor::Ruby::RubyEngine
Data    = DataEditor::Ruby::RubyData.Instance

def Builder.Add(type,parameter,&block)
	Builder.Push(type,parameter,block)
end

