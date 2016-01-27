# Weka机器学习工具使用助手

Weka有自己的数据类型，分别是Numeric数值型、Norminal标称型、String文本型、DateTime日期型。
Weka导入的arff格式文件，需要在其中设置每列属于哪种类型的字段。
本工具用来简化这一操作，它可以将一个普通的csv文件转换成Weka需要的arff文件。
目前工程刚刚建立，每列的类型还需要在工具中手动选择。

###对于导入的csv文件的要求

1. 第一行为标题列
2. 列名不能有重复
3. 各单元格值不能为空
4. 文件编码格式为utf-8

下载地址：[Weka助手.exe](https://raw.githubusercontent.com/liyumeng/WekaHelperProject/master/%E5%8F%AF%E6%89%A7%E8%A1%8C%E6%96%87%E4%BB%B6/Weka%E5%B0%8F%E5%8A%A9%E6%89%8B.exe)
