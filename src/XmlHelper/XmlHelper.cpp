// XmlHelper.cpp : Defines the entry point for the console application.
//


#include "stdafx.h"
#include <iomanip>
#include <locale>
#include <sstream>
#include <strstream>
#include <iostream>
#include <fstream>
#include <string>
#include <map>
#include "XmlHelper.h"
using namespace std;



int main(int argc, char* argv[])
{
    string argument0 = argv[0];

    string argument1 = argv[1];

    ifstream is(argument1, ios::in);

    int i = 0;

    char buf[4096];
    do {
        i++;
        is.read(buf, sizeof(buf));
        process_chunk(buf, is.gcount());

        

    } while (is);


    cin.get();

    return 0;
}

void process_chunk(char buffer[], streamsize count)
{

    string builder = string();

    string chunk = string(buffer);

    int indentLevel = 0;

    int length = chunk.length();

    for (int i = 0; i < length; i++)
    {
        int position = i;
        char previous;
        char character = chunk[i];

        switch (character)
        {
        case '<':
        {



            char next;
            if ((i + 1) < length)
            {
                next = chunk[i + 1];
                if (next == '/')
                {
                    indentLevel--;
                }
            }
            for (int j = 0; j < indentLevel - 1; j++)
            {
                builder += INDENT;
            }

            builder += '<';
            indentLevel++;
            break;
        }
        case '>':
        {
            builder += ">\r\n";
            break;
        }
        case '/':
        {   char prev;
            if ((i - 1) > -1)
            {
                prev = chunk[i - 1];
                if (prev == '<')
                {
                    indentLevel--;

                }
            }
            char next;
            if ((i + 1) < length)
            {
                next = chunk[i + 1];
                if (next == '>')
                {
                    indentLevel--;
                }
            }
            builder += '/';
        break;
        }
        default:
        {
            builder += character;
            break;
        }
        }

        previous = character;
    }

    cout << builder << endl;

}

string getIndentedNewLine(int indentLevel, string indent)
{
    if (indentLevel == 0) return string("\r\n");

    string line;
    line += "\r\n";
    for (size_t i = indentLevel; i != 0; i--)
    {
        line = line + indent;
    }
    indentLevel = 0;
    return line;
}

string getElementName(string text, int start)
{
    string elementName = string();

    char current = text[start];

    while (current != ' ')
    {
        start++;
        elementName += current;
        current = text[start];
    }

    return elementName;
}



string Convert(int number)
{
    string result;

    ostringstream converter;

    converter << number;

    result = converter.str();

    return converter.str();
}