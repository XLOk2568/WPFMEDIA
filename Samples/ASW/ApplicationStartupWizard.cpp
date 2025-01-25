#include <iostream>
#include <filesystem>
#include <fstream>
#include <windows.h>
#include <iostream>
#include <string>
#include <cstdlib>
namespace fs = std::filesystem;
void createFileWithContent(const fs::path& filePath, const std::string& content) {
    std::ofstream file(filePath);
    if (file.is_open()) {
        file << content;
        file.close();
    }
    else {
        std::cerr << "Unable to create file: " << filePath << std::endl;
    }
}

int main() {
    fs::path currentPath = fs::current_path();
    fs::path testDir = currentPath / "TEST";
    fs::path testFile = testDir / "1.txt";
    std::string content = "11111*{@}67567";
    // 检测是否存在TEST文件夹
    if (fs::exists(testDir)) 
    {
        std::cout << "存在TEST文件夹" << std::endl;
        // 检测TEST文件夹下是否存在1.txt文件
        if (fs::exists(testFile)) 
        {
            std::cout << "存在1.txt文件" << std::endl;
        }
        else 
        {
            std::cout << "不存在1.txt文件" << std::endl;
            createFileWithContent(testFile, content);
        }
    }
    else 
    {
        std::cout << "不存在TEST文件夹" << std::endl;
        fs::create_directory(testDir);
        createFileWithContent(testFile, content);
    }
    {
        fs::path appPath_Frist = fs::current_path() / "app.exe";
        if (fs::exists(appPath_Frist))
        {
          //fs::path currentPath = fs::current_path();            
            //std::string runTime = (currentPath / "app.exe").string();
            //std::cout << "启动new文件夹下的app.exe" << std::endl;
            // 获取路径
            //std::string path;
            //std::cout << "请输入程序路径: ";
            //std::getline(std::cin, path);
            // 启动程序
            //std::string command = "\"" + path + "\"";
            //std::system(command.c_str());
            //exit(0);
            //return 0;        
        }
        else
        {
            std::cout << "未找到new文件夹下的app.exe" << std::endl;
        }
        return 0;
    }
}

