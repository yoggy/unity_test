#!/usr/bin/ruby
require 'fileutils'

Dir.chdir(File.dirname($0))

FileUtils.cp('build/libs/libtestservice.jar', '../../Assets/Plugins/Android/', {:verbose => true})
