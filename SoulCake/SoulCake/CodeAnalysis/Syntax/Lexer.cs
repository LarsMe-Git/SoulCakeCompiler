﻿using SoulCake.CodeAnalysis.Text;
using System.Collections.Generic;

namespace SoulCake.CodeAnalysis.Syntax
{
    internal sealed class Lexer
    {

        private readonly DiagnosticBag _diagnostics = new DiagnosticBag();
        private readonly SourceText _text;
        

        private int _position;

        private int _start;
        private SyntaxKind _kind;
        private object _value;
        

        public Lexer(SourceText text)
        {
            _text = text;
        }

        public DiagnosticBag Diagnostics => _diagnostics;

        private char Current => Peek(0);
        private char Lookahead => Peek(1);

        private char Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _text.Length)
            {
                return '\0';
            }
            return _text[index];
        }

        public SyntaxToken Lex()
        {
            // numbers
            // + - * / ()
            //whitespace

            _start = _position;
            _kind = SyntaxKind.BadToken;
            _value = null;

            //keep reading numbers, create word that represents the number

                switch (Current)
                {
                    case '\0':
                        _kind = SyntaxKind.EndOfFileToken;
                        break;
                    case '+':
                        _kind = SyntaxKind.PlusToken;
                        _position++;
                        break;
                    case '-':
                        _kind = SyntaxKind.MinusToken;
                        _position++;
                        break;
                    case '*':
                        _kind = SyntaxKind.StarToken;
                        _position++;
                        break;
                    case '/':
                       _kind = SyntaxKind.SlashToken;
                       _position++;
                        break;
                    case '(':
                        _kind = SyntaxKind.OpenParenthesisToken;
                        _position++;
                        break;
                    case ')':
                        _kind = SyntaxKind.CloseParenthesisToken;
                        _position++;
                        break;
                    case '&':
                        if (Lookahead == '&')
                        {
                            _kind = SyntaxKind.AmpersandAmpersandToken;
                            _position += 2;
                            break;
                        }
                        break;
                    case '|':
                        if (Lookahead == '|')
                        {
                            _kind = SyntaxKind.PipePipeToken;
                            _position += 2;
                            break;
                        }
                        break;
                    case '=':
                        _position++;
                        if (Current != '=')
                        {
                            _kind = SyntaxKind.EqualsToken;
                        }
                        else
                        {
                            _position++;
                            _kind = SyntaxKind.EqualsEqualsToken;
                        }
                        break;
                    case '!':
                        _position++;
                        if (Current != '=')
                        {
                            _kind = SyntaxKind.BangToken;
                        }
                        else
                        {
                            _kind = SyntaxKind.BangEqualsToken;
                            _position++;
                        }
                        break;

                    case '0': case '1': case '2': case '3': case '4':
                    case '5': case '6': case '7': case '8': case '9':
                        ReadNumberToken();
                        break;
                    //most common white spaces
                    case ' ':
                    case '\t':
                    case '\n':
                    case '\r':
                        ReadWhiteSpace();
                        break;
                default:
                       if (char.IsLetter(Current))
                       {
                            ReadIdentifierOrKeyword();
                       }
                       else if (char.IsWhiteSpace(Current))
                       {
                        ReadWhiteSpace();
                       }
                       else
                       {
                        _diagnostics.ReportBadCharacter(_position, Current);
                        _position++;
                       }
                        break;
                }

            var length = _position - _start;
            var text = SyntaxFacts.GetText(_kind);
            if (text == null)
            {
                text = _text.ToString(_start, length);
            }

            return new SyntaxToken(_kind, _start, text, _value);
        }

        private void ReadWhiteSpace()
        {
            while (char.IsWhiteSpace(Current))
            {
                _position++;
            }

            _kind = SyntaxKind.WhitespaceToken;
        }

        private void ReadNumberToken()
        {
            while (char.IsDigit(Current))
            {
                _position++;
            }

            var length = _position - _start;
            var text = _text.ToString(_start, length);
            if (!int.TryParse(text, out var value))
            {
                _diagnostics.ReportInvalidNumber(new TextSpan(_start, length), text, typeof(int));

            }

            _value = value;
            _kind = SyntaxKind.NumberToken;
         
        }
        private void ReadIdentifierOrKeyword()
        {
            while (char.IsLetter(Current))
            {
                _position++;
            }

            var length = _position - _start;
            var text = _text.ToString(_start, length);
            _kind = SyntaxFacts.GetKeyWordKind(text);

        }
    }



}