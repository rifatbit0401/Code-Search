using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeClone
{
    public static class CodeKeyword
    {
        public const string INT = "int";
        public const string DOUBLE = "double";
        public const string FLOAT = "float";
        public const string STRING = "string";

        public const string FOR = "for";
        public const string WHILE = "while";

        public const string IF = "if";
        public const string ELSE_IF = "else if";
        public const string ELSE = "else";
        public const string SWITCH = "switch";
        public const string FUNC_CALL_REGEX = @"[a-zA-Z]+\([^\)]*\)(\.[^\)]*\))?";

        public const string ADDITION = "+";
        public const string SUBTRACTION = "-";
        public const string DIVISION = "/";
        public const string MULTIPLICATION = "*";

    }

    public enum Tag
    {
        DECLARATION,
        STATEMENT,
        FOR,
        WHILE,
        IF,
        ELSE_IF,
        ELSE,
        FUNCTION_CALL,


        ADDITION,
        MULTIPLICATION,
        SUBTRACTION,
        DIVISION,


        TAB,
        NOT_FOUND
    }


    public class Rule
    {

        public Tag GetTag(string line)
        {
            line = line.Trim();
            if (IsDeclaration(line))
                return Tag.DECLARATION;
            if (IsForLoop(line))
                return Tag.FOR;
            if (IsWhileLoop(line))
                return Tag.WHILE;
            if (IsIfStatement(line))
                return Tag.IF;
            if (IsElseIfStatement(line))
                return Tag.ELSE_IF;
            if (IsElseStatement(line))
                return Tag.ELSE;
            if (IsFunctionCall(line))
                return Tag.FUNCTION_CALL;

            if (IsAddOperation(line))
                return Tag.ADDITION;
            if (IsSubtractOperation(line))
                return Tag.SUBTRACTION;
            if (IsDivideOperation(line))
                return Tag.DIVISION;
            if (IsMultiplyOperation(line))
                return Tag.MULTIPLICATION;

            if (IsStatement(line))
                return Tag.STATEMENT;
            
            return Tag.NOT_FOUND;
        }

        public bool IsDeclaration(string line)
        {
            return line.StartsWith(CodeKeyword.INT) || line.StartsWith(CodeKeyword.DOUBLE) ||
                   line.StartsWith(CodeKeyword.FLOAT) || line.StartsWith(CodeKeyword.STRING);
        }
        public bool IsStatement(string line)
        {
            return line.EndsWith(";");
        }

        public bool IsForLoop(string line)
        {
            return line.StartsWith(CodeKeyword.FOR);
        }

        public bool IsWhileLoop(string line)
        {
            return line.StartsWith(CodeKeyword.WHILE);
        }
        public bool IsIfStatement(string line)
        {
            return line.StartsWith(CodeKeyword.IF);
        }
        public bool IsElseIfStatement(string line)
        {
            return line.StartsWith(CodeKeyword.ELSE_IF);
        }
        public bool IsElseStatement(string line)
        {
            return line.StartsWith(CodeKeyword.ELSE);
        }

        public bool IsFunctionCall(string line)
        {
            return Regex.IsMatch(line, CodeKeyword.FUNC_CALL_REGEX);
        }

        public bool IsAddOperation(string line)
        {
            return line.Contains(CodeKeyword.ADDITION);
        }

        public bool IsSubtractOperation(string line)
        {
            return line.Contains(CodeKeyword.SUBTRACTION);
        }
        public bool IsDivideOperation(string line)
        {
            return line.Contains(CodeKeyword.DIVISION);
        }
        public bool IsMultiplyOperation(string line)
        {
            return line.Contains(CodeKeyword.MULTIPLICATION);
        }
    }
}
