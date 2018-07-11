#pragma once

#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <stack>

#define INDENT "  "

using namespace std;


void process_chunk(char buffer[], streamsize count);


string getIndentedNewLine(int indentLevel, string indent);

string Convert(int number);

string getElementName(string text, int start);


class SourceText
{

};


struct TextSpan
{

};

struct TextLine
{
public:
    string Text;
    int LineNumber;
    int Start;
    int End;
    int LineBreakLength;
    int EndIncludingLineBreak;
    TextSpan Span;
    TextSpan SpanIncludingLineBreak;


    bool Equals(TextLine other)
    {
        return other.Text == Text
            && other.Start == Start
            && other.EndIncludingLineBreak == EndIncludingLineBreak;
    }

    bool operator ==(TextLine other)
    {
        return this->Equals(other);
    }

    bool operator !=(TextLine other)
    {
        return !this->Equals(other);
    }
};



