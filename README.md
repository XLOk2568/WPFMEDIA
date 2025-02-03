# WPFMEDIA(简中)
#### 一个初学者都能看懂的项目，net8&C++


## 简介


### 1.便捷化设计，不会向C盘或者注册表写入文件，仅仅在程序运行目录下存储个性化和程序配置。更话系统或者更换设备，仅需要将目录移动到目标即可。一键即可设置为默认关联程序，支持列表拖拽，支持音量滚轮滑动控制


### 2.个性化设计，使用SlopeOne在GPU(支持CUDA(仅NVIDIA)和OpenCl(大部分CPU与GPU))本地计算机上进行个性化推荐算法，保护你的隐私和节约时间


### 3.除更新部分需要联网(当然，更新实现是使用2018k.cn实现的)外，其他均在你的计算机本地上运行


### 4.多语言支持，支持中，英，日，法语言

## 说明
### 1.GPU加速用在双向冒泡排序和SlopeOne上，当然，GPU工作的的地方大部分都优化成为CPU与GPU共同工作，以提高效率


### 2.数据存储在MediaFastPlayer.exe目录的Appdata和user_m...中，迁移数据仅需要移动或者复制即可。当然，也可以将start_first.exe下的main文件夹和start_dirst.exe共同移动或者复制到新路径即可

## 使用了这些项目


[ILGPU](https://ilgpu.net/)(用于GPU加速，主要用于一些算法加速)


[iNKORE.UI.WPF.Modern](https://github.com/iNKORE-NET/UI.WPF.Modern/)(现代化UI)


[UpdateD](https://github.com/YUXUAN888/UpdateD)(用于更新，本项目上改成了使用http的方法使用)


[TagLibSharp](https://github.com/mono/taglib-sharp)(检索歌曲信息)


[]
############### 后期将移植到winui3，有空就做安卓版
