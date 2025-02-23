// Generated by TinyPG v1.3 available at www.codeproject.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace kOS.Safe.Compilation.KS
{
    #region Scanner

    public partial class Scanner
    {
        public string Input;
        public string LowerInput;
        public int StartPos = 0;
        public int EndPos = 0;
        public string CurrentFile;
        public int CurrentLine;
        public int CurrentColumn;
        public int CurrentPosition;
        public List<Token> Skipped; // tokens that were skipped
        public Dictionary<TokenType, Regex> Patterns;

        private Token LookAheadToken;
        private List<TokenType> Tokens;
        private List<TokenType> SkipList; // tokens to be skipped
        private readonly TokenType FileAndLine;

        public Scanner()
        {
            Regex regex;
            Patterns = new Dictionary<TokenType, Regex>();
            Tokens = new List<TokenType>();
            LookAheadToken = null;
            Skipped = new List<Token>();

            SkipList = new List<TokenType>();
            SkipList.Add(TokenType.WHITESPACE);
            SkipList.Add(TokenType.COMMENTLINE);

            regex = new Regex(@"\G(?:(\+|-))");
            Patterns.Add(TokenType.PLUSMINUS, regex);
            Tokens.Add(TokenType.PLUSMINUS);

            regex = new Regex(@"\G(?:\*)");
            Patterns.Add(TokenType.MULT, regex);
            Tokens.Add(TokenType.MULT);

            regex = new Regex(@"\G(?:/)");
            Patterns.Add(TokenType.DIV, regex);
            Tokens.Add(TokenType.DIV);

            regex = new Regex(@"\G(?:\^)");
            Patterns.Add(TokenType.POWER, regex);
            Tokens.Add(TokenType.POWER);

            regex = new Regex(@"\G(?:e((?=\d)|\b))");
            Patterns.Add(TokenType.E, regex);
            Tokens.Add(TokenType.E);

            regex = new Regex(@"\G(?:not\b)");
            Patterns.Add(TokenType.NOT, regex);
            Tokens.Add(TokenType.NOT);

            regex = new Regex(@"\G(?:and\b)");
            Patterns.Add(TokenType.AND, regex);
            Tokens.Add(TokenType.AND);

            regex = new Regex(@"\G(?:or\b)");
            Patterns.Add(TokenType.OR, regex);
            Tokens.Add(TokenType.OR);

            regex = new Regex(@"\G(?:true\b|\bfalse\b)");
            Patterns.Add(TokenType.TRUEFALSE, regex);
            Tokens.Add(TokenType.TRUEFALSE);

            regex = new Regex(@"\G(?:<>|>=|<=|=|>|<)");
            Patterns.Add(TokenType.COMPARATOR, regex);
            Tokens.Add(TokenType.COMPARATOR);

            regex = new Regex(@"\G(?:set\b)");
            Patterns.Add(TokenType.SET, regex);
            Tokens.Add(TokenType.SET);

            regex = new Regex(@"\G(?:to\b)");
            Patterns.Add(TokenType.TO, regex);
            Tokens.Add(TokenType.TO);

            regex = new Regex(@"\G(?:is\b)");
            Patterns.Add(TokenType.IS, regex);
            Tokens.Add(TokenType.IS);

            regex = new Regex(@"\G(?:if\b)");
            Patterns.Add(TokenType.IF, regex);
            Tokens.Add(TokenType.IF);

            regex = new Regex(@"\G(?:else\b)");
            Patterns.Add(TokenType.ELSE, regex);
            Tokens.Add(TokenType.ELSE);

            regex = new Regex(@"\G(?:until\b)");
            Patterns.Add(TokenType.UNTIL, regex);
            Tokens.Add(TokenType.UNTIL);

            regex = new Regex(@"\G(?:step\b)");
            Patterns.Add(TokenType.STEP, regex);
            Tokens.Add(TokenType.STEP);

            regex = new Regex(@"\G(?:do\b)");
            Patterns.Add(TokenType.DO, regex);
            Tokens.Add(TokenType.DO);

            regex = new Regex(@"\G(?:lock\b)");
            Patterns.Add(TokenType.LOCK, regex);
            Tokens.Add(TokenType.LOCK);

            regex = new Regex(@"\G(?:unlock\b)");
            Patterns.Add(TokenType.UNLOCK, regex);
            Tokens.Add(TokenType.UNLOCK);

            regex = new Regex(@"\G(?:print\b)");
            Patterns.Add(TokenType.PRINT, regex);
            Tokens.Add(TokenType.PRINT);

            regex = new Regex(@"\G(?:at\b)");
            Patterns.Add(TokenType.AT, regex);
            Tokens.Add(TokenType.AT);

            regex = new Regex(@"\G(?:on\b)");
            Patterns.Add(TokenType.ON, regex);
            Tokens.Add(TokenType.ON);

            regex = new Regex(@"\G(?:toggle\b)");
            Patterns.Add(TokenType.TOGGLE, regex);
            Tokens.Add(TokenType.TOGGLE);

            regex = new Regex(@"\G(?:wait\b)");
            Patterns.Add(TokenType.WAIT, regex);
            Tokens.Add(TokenType.WAIT);

            regex = new Regex(@"\G(?:when\b)");
            Patterns.Add(TokenType.WHEN, regex);
            Tokens.Add(TokenType.WHEN);

            regex = new Regex(@"\G(?:then\b)");
            Patterns.Add(TokenType.THEN, regex);
            Tokens.Add(TokenType.THEN);

            regex = new Regex(@"\G(?:off\b)");
            Patterns.Add(TokenType.OFF, regex);
            Tokens.Add(TokenType.OFF);

            regex = new Regex(@"\G(?:stage\b)");
            Patterns.Add(TokenType.STAGE, regex);
            Tokens.Add(TokenType.STAGE);

            regex = new Regex(@"\G(?:clearscreen\b)");
            Patterns.Add(TokenType.CLEARSCREEN, regex);
            Tokens.Add(TokenType.CLEARSCREEN);

            regex = new Regex(@"\G(?:add\b)");
            Patterns.Add(TokenType.ADD, regex);
            Tokens.Add(TokenType.ADD);

            regex = new Regex(@"\G(?:remove\b)");
            Patterns.Add(TokenType.REMOVE, regex);
            Tokens.Add(TokenType.REMOVE);

            regex = new Regex(@"\G(?:log\b)");
            Patterns.Add(TokenType.LOG, regex);
            Tokens.Add(TokenType.LOG);

            regex = new Regex(@"\G(?:break\b)");
            Patterns.Add(TokenType.BREAK, regex);
            Tokens.Add(TokenType.BREAK);

            regex = new Regex(@"\G(?:preserve\b)");
            Patterns.Add(TokenType.PRESERVE, regex);
            Tokens.Add(TokenType.PRESERVE);

            regex = new Regex(@"\G(?:declare\b)");
            Patterns.Add(TokenType.DECLARE, regex);
            Tokens.Add(TokenType.DECLARE);

            regex = new Regex(@"\G(?:defined\b)");
            Patterns.Add(TokenType.DEFINED, regex);
            Tokens.Add(TokenType.DEFINED);

            regex = new Regex(@"\G(?:local\b)");
            Patterns.Add(TokenType.LOCAL, regex);
            Tokens.Add(TokenType.LOCAL);

            regex = new Regex(@"\G(?:global\b)");
            Patterns.Add(TokenType.GLOBAL, regex);
            Tokens.Add(TokenType.GLOBAL);

            regex = new Regex(@"\G(?:parameter\b)");
            Patterns.Add(TokenType.PARAMETER, regex);
            Tokens.Add(TokenType.PARAMETER);

            regex = new Regex(@"\G(?:function\b)");
            Patterns.Add(TokenType.FUNCTION, regex);
            Tokens.Add(TokenType.FUNCTION);

            regex = new Regex(@"\G(?:return\b)");
            Patterns.Add(TokenType.RETURN, regex);
            Tokens.Add(TokenType.RETURN);

            regex = new Regex(@"\G(?:switch\b)");
            Patterns.Add(TokenType.SWITCH, regex);
            Tokens.Add(TokenType.SWITCH);

            regex = new Regex(@"\G(?:copy\b)");
            Patterns.Add(TokenType.COPY, regex);
            Tokens.Add(TokenType.COPY);

            regex = new Regex(@"\G(?:from\b)");
            Patterns.Add(TokenType.FROM, regex);
            Tokens.Add(TokenType.FROM);

            regex = new Regex(@"\G(?:rename\b)");
            Patterns.Add(TokenType.RENAME, regex);
            Tokens.Add(TokenType.RENAME);

            regex = new Regex(@"\G(?:volume\b)");
            Patterns.Add(TokenType.VOLUME, regex);
            Tokens.Add(TokenType.VOLUME);

            regex = new Regex(@"\G(?:file\b)");
            Patterns.Add(TokenType.FILE, regex);
            Tokens.Add(TokenType.FILE);

            regex = new Regex(@"\G(?:delete\b)");
            Patterns.Add(TokenType.DELETE, regex);
            Tokens.Add(TokenType.DELETE);

            regex = new Regex(@"\G(?:edit\b)");
            Patterns.Add(TokenType.EDIT, regex);
            Tokens.Add(TokenType.EDIT);

            regex = new Regex(@"\G(?:run\b)");
            Patterns.Add(TokenType.RUN, regex);
            Tokens.Add(TokenType.RUN);

            regex = new Regex(@"\G(?:runpath\b)");
            Patterns.Add(TokenType.RUNPATH, regex);
            Tokens.Add(TokenType.RUNPATH);

            regex = new Regex(@"\G(?:runoncepath\b)");
            Patterns.Add(TokenType.RUNONCEPATH, regex);
            Tokens.Add(TokenType.RUNONCEPATH);

            regex = new Regex(@"\G(?:once\b)");
            Patterns.Add(TokenType.ONCE, regex);
            Tokens.Add(TokenType.ONCE);

            regex = new Regex(@"\G(?:compile\b)");
            Patterns.Add(TokenType.COMPILE, regex);
            Tokens.Add(TokenType.COMPILE);

            regex = new Regex(@"\G(?:list\b)");
            Patterns.Add(TokenType.LIST, regex);
            Tokens.Add(TokenType.LIST);

            regex = new Regex(@"\G(?:reboot\b)");
            Patterns.Add(TokenType.REBOOT, regex);
            Tokens.Add(TokenType.REBOOT);

            regex = new Regex(@"\G(?:shutdown\b)");
            Patterns.Add(TokenType.SHUTDOWN, regex);
            Tokens.Add(TokenType.SHUTDOWN);

            regex = new Regex(@"\G(?:for\b)");
            Patterns.Add(TokenType.FOR, regex);
            Tokens.Add(TokenType.FOR);

            regex = new Regex(@"\G(?:unset\b)");
            Patterns.Add(TokenType.UNSET, regex);
            Tokens.Add(TokenType.UNSET);

            regex = new Regex(@"\G(?:choose\b)");
            Patterns.Add(TokenType.CHOOSE, regex);
            Tokens.Add(TokenType.CHOOSE);

            regex = new Regex(@"\G(?:\()");
            Patterns.Add(TokenType.BRACKETOPEN, regex);
            Tokens.Add(TokenType.BRACKETOPEN);

            regex = new Regex(@"\G(?:\))");
            Patterns.Add(TokenType.BRACKETCLOSE, regex);
            Tokens.Add(TokenType.BRACKETCLOSE);

            regex = new Regex(@"\G(?:\{)");
            Patterns.Add(TokenType.CURLYOPEN, regex);
            Tokens.Add(TokenType.CURLYOPEN);

            regex = new Regex(@"\G(?:\})");
            Patterns.Add(TokenType.CURLYCLOSE, regex);
            Tokens.Add(TokenType.CURLYCLOSE);

            regex = new Regex(@"\G(?:\[)");
            Patterns.Add(TokenType.SQUAREOPEN, regex);
            Tokens.Add(TokenType.SQUAREOPEN);

            regex = new Regex(@"\G(?:\])");
            Patterns.Add(TokenType.SQUARECLOSE, regex);
            Tokens.Add(TokenType.SQUARECLOSE);

            regex = new Regex(@"\G(?:,)");
            Patterns.Add(TokenType.COMMA, regex);
            Tokens.Add(TokenType.COMMA);

            regex = new Regex(@"\G(?::)");
            Patterns.Add(TokenType.COLON, regex);
            Tokens.Add(TokenType.COLON);

            regex = new Regex(@"\G(?:in\b)");
            Patterns.Add(TokenType.IN, regex);
            Tokens.Add(TokenType.IN);

            regex = new Regex(@"\G(?:#)");
            Patterns.Add(TokenType.ARRAYINDEX, regex);
            Tokens.Add(TokenType.ARRAYINDEX);

            regex = new Regex(@"\G(?:all\b)");
            Patterns.Add(TokenType.ALL, regex);
            Tokens.Add(TokenType.ALL);

            regex = new Regex(@"\G(?:[_\p{L}]\w*)");
            Patterns.Add(TokenType.IDENTIFIER, regex);
            Tokens.Add(TokenType.IDENTIFIER);

            regex = new Regex(@"\G(?:[_\p{L}]\w*(\.[_\p{L}]\w*)*)");
            Patterns.Add(TokenType.FILEIDENT, regex);
            Tokens.Add(TokenType.FILEIDENT);

            regex = new Regex(@"\G(?:\d[_\d]*)");
            Patterns.Add(TokenType.INTEGER, regex);
            Tokens.Add(TokenType.INTEGER);

            regex = new Regex(@"\G(?:(\d+(?:_\d*)*)?\.\d+(?:_\d*)*)");
            Patterns.Add(TokenType.DOUBLE, regex);
            Tokens.Add(TokenType.DOUBLE);

            regex = new Regex(@"\G(?:@?\""(\""\""|[^\""])*\"")");
            Patterns.Add(TokenType.STRING, regex);
            Tokens.Add(TokenType.STRING);

            regex = new Regex(@"\G(?:\.)");
            Patterns.Add(TokenType.EOI, regex);
            Tokens.Add(TokenType.EOI);

            regex = new Regex(@"\G(?:@)");
            Patterns.Add(TokenType.ATSIGN, regex);
            Tokens.Add(TokenType.ATSIGN);

            regex = new Regex(@"\G(?:lazyglobal\b)");
            Patterns.Add(TokenType.LAZYGLOBAL, regex);
            Tokens.Add(TokenType.LAZYGLOBAL);

            regex = new Regex(@"\G(?:clobberbuiltins\b)");
            Patterns.Add(TokenType.CLOBBERBUILTINS, regex);
            Tokens.Add(TokenType.CLOBBERBUILTINS);

            regex = new Regex(@"\G(?:$)");
            Patterns.Add(TokenType.EOF, regex);
            Tokens.Add(TokenType.EOF);

            regex = new Regex(@"\G(?:(\s|\p{C})+)");
            Patterns.Add(TokenType.WHITESPACE, regex);
            Tokens.Add(TokenType.WHITESPACE);

            regex = new Regex(@"\G(?://[^\n]*\n?)");
            Patterns.Add(TokenType.COMMENTLINE, regex);
            Tokens.Add(TokenType.COMMENTLINE);


        }

        public void Init(string input)
        {
            Init(input, "");
        }

        public void Init(string input, string fileName)
        {
            this.Input = input;
            LowerInput = Input.ToLower();
            StartPos = 0;
            EndPos = 0;
            CurrentFile = fileName;
            CurrentLine = 1;
            CurrentColumn = 1;
            CurrentPosition = 0;
            LookAheadToken = null;
        }

        public Token GetToken(TokenType type)
        {
            Token t = new Token(this.StartPos, this.EndPos);
            t.Type = type;
            return t;
        }

         /// <summary>
        /// executes a lookahead of the next token
        /// and will advance the scan on the input string
        /// </summary>
        /// <returns></returns>
        public Token Scan(params TokenType[] expectedtokens)
        {
            Token tok = LookAhead(expectedtokens); // temporarely retrieve the lookahead
            LookAheadToken = null; // reset lookahead token, so scanning will continue
            StartPos = tok.EndPos;
            EndPos = tok.EndPos; // set the tokenizer to the new scan position
            CurrentLine = tok.Line + (tok.Text.Length - tok.Text.Replace("\n", "").Length);
            CurrentFile = tok.File;
            return tok;
        }

        /// <summary>
        /// returns token with longest best match
        /// </summary>
        /// <returns></returns>
        public Token LookAhead(params TokenType[] expectedtokens)
        {
            int i;
            int startpos = StartPos;
            int endpos = EndPos;
            int currentline = CurrentLine;
            string currentFile = CurrentFile;
            Token tok = null;
            List<TokenType> scantokens;


            // this prevents double scanning and matching
            // increased performance
            if (LookAheadToken != null 
                && LookAheadToken.Type != TokenType._UNDETERMINED_ 
                && LookAheadToken.Type != TokenType._NONE_) return LookAheadToken;

            // if no scantokens specified, then scan for all of them (= backward compatible)
            if (expectedtokens.Length == 0)
                scantokens = Tokens;
            else
            {
                scantokens = new List<TokenType>(expectedtokens);
                scantokens.AddRange(SkipList);
            }

            do
            {

                int len = -1;
                TokenType index = (TokenType)int.MaxValue;

                tok = new Token(startpos, endpos);

                for (i = 0; i < scantokens.Count; i++)
                {
                    Regex r = Patterns[scantokens[i]];
                    // use startpos instead of a substring to save memory allocation, but anchors must use \G in place of ^
                    Match m = r.Match(LowerInput, startpos);
                    if (m.Success && m.Index == startpos && ((m.Length > len) || (scantokens[i] < index && m.Length == len )))
                    {
                        len = m.Length;
                        index = scantokens[i];  
                    }
                }

                if (index >= 0 && len >= 0)
                {
                    tok.EndPos = startpos + len;
                    tok.Text = Input.Substring(tok.StartPos, len);
                    tok.Type = index;
                }
                else if (tok.StartPos == tok.EndPos)
                {
                    if (tok.StartPos < Input.Length)
                        tok.Text = Input.Substring(tok.StartPos, 1);
                    else
                        tok.Text = "EOF";
                }

                // Update the line and column count for error reporting.
                tok.File = currentFile;
                tok.Line = currentline;
                if (tok.StartPos < Input.Length)
                    tok.Column = tok.StartPos - Input.LastIndexOf('\n', tok.StartPos);

                if (SkipList.Contains(tok.Type))
                {
                    startpos = tok.EndPos;
                    endpos = tok.EndPos;
                    currentline = tok.Line + (tok.Text.Length - tok.Text.Replace("\n", "").Length);
                    currentFile = tok.File;
                    Skipped.Add(tok);
                }
                else
                {
                    // only assign to non-skipped tokens
                    tok.Skipped = Skipped; // assign prior skips to this token
                    Skipped = new List<Token>(); //reset skips
                }

                // Check to see if the parsed token wants to 
                // alter the file and line number.
                if (tok.Type == FileAndLine)
                {
                    var match = Patterns[tok.Type].Match(tok.Text);
                    var fileMatch = match.Groups["File"];
                    if (fileMatch.Success)
                        currentFile = fileMatch.Value.Replace("\\\\", "\\");
                    var lineMatch = match.Groups["Line"];
                    if (lineMatch.Success)
                        currentline = int.Parse(lineMatch.Value, NumberStyles.Integer, CultureInfo.InvariantCulture);
                }
            }
            while (SkipList.Contains(tok.Type));

            LookAheadToken = tok;
            return tok;
        }
    }

    #endregion

    #region Token

    public enum TokenType
    {

            //Non terminal tokens:
            _NONE_  = 0,
            _UNDETERMINED_= 1,

            //Non terminal tokens:
            Start   = 2,
            instruction_block= 3,
            instruction= 4,
            lazyglobal_directive= 5,
            clobberbuiltins_directive= 6,
            directive= 7,
            empty_stmt= 8,
            set_stmt= 9,
            if_stmt = 10,
            until_stmt= 11,
            fromloop_stmt= 12,
            unlock_stmt= 13,
            print_stmt= 14,
            on_stmt = 15,
            toggle_stmt= 16,
            wait_stmt= 17,
            when_stmt= 18,
            onoff_stmt= 19,
            onoff_trailer= 20,
            stage_stmt= 21,
            clear_stmt= 22,
            add_stmt= 23,
            remove_stmt= 24,
            log_stmt= 25,
            break_stmt= 26,
            preserve_stmt= 27,
            declare_identifier_clause= 28,
            declare_parameter_clause= 29,
            declare_function_clause= 30,
            declare_lock_clause= 31,
            declare_stmt= 32,
            return_stmt= 33,
            switch_stmt= 34,
            copy_stmt= 35,
            rename_stmt= 36,
            delete_stmt= 37,
            edit_stmt= 38,
            run_stmt= 39,
            runpath_stmt= 40,
            runoncepath_stmt= 41,
            compile_stmt= 42,
            list_stmt= 43,
            reboot_stmt= 44,
            shutdown_stmt= 45,
            for_stmt= 46,
            unset_stmt= 47,
            arglist = 48,
            expr    = 49,
            ternary_expr= 50,
            or_expr = 51,
            and_expr= 52,
            compare_expr= 53,
            arith_expr= 54,
            multdiv_expr= 55,
            unary_expr= 56,
            factor  = 57,
            suffix  = 58,
            suffix_trailer= 59,
            suffixterm= 60,
            suffixterm_trailer= 61,
            function_trailer= 62,
            array_trailer= 63,
            atom    = 64,
            sci_number= 65,
            number  = 66,
            varidentifier= 67,
            identifier_led_stmt= 68,
            identifier_led_expr= 69,

            //Terminal tokens:
            PLUSMINUS= 70,
            MULT    = 71,
            DIV     = 72,
            POWER   = 73,
            E       = 74,
            NOT     = 75,
            AND     = 76,
            OR      = 77,
            TRUEFALSE= 78,
            COMPARATOR= 79,
            SET     = 80,
            TO      = 81,
            IS      = 82,
            IF      = 83,
            ELSE    = 84,
            UNTIL   = 85,
            STEP    = 86,
            DO      = 87,
            LOCK    = 88,
            UNLOCK  = 89,
            PRINT   = 90,
            AT      = 91,
            ON      = 92,
            TOGGLE  = 93,
            WAIT    = 94,
            WHEN    = 95,
            THEN    = 96,
            OFF     = 97,
            STAGE   = 98,
            CLEARSCREEN= 99,
            ADD     = 100,
            REMOVE  = 101,
            LOG     = 102,
            BREAK   = 103,
            PRESERVE= 104,
            DECLARE = 105,
            DEFINED = 106,
            LOCAL   = 107,
            GLOBAL  = 108,
            PARAMETER= 109,
            FUNCTION= 110,
            RETURN  = 111,
            SWITCH  = 112,
            COPY    = 113,
            FROM    = 114,
            RENAME  = 115,
            VOLUME  = 116,
            FILE    = 117,
            DELETE  = 118,
            EDIT    = 119,
            RUN     = 120,
            RUNPATH = 121,
            RUNONCEPATH= 122,
            ONCE    = 123,
            COMPILE = 124,
            LIST    = 125,
            REBOOT  = 126,
            SHUTDOWN= 127,
            FOR     = 128,
            UNSET   = 129,
            CHOOSE  = 130,
            BRACKETOPEN= 131,
            BRACKETCLOSE= 132,
            CURLYOPEN= 133,
            CURLYCLOSE= 134,
            SQUAREOPEN= 135,
            SQUARECLOSE= 136,
            COMMA   = 137,
            COLON   = 138,
            IN      = 139,
            ARRAYINDEX= 140,
            ALL     = 141,
            IDENTIFIER= 142,
            FILEIDENT= 143,
            INTEGER = 144,
            DOUBLE  = 145,
            STRING  = 146,
            EOI     = 147,
            ATSIGN  = 148,
            LAZYGLOBAL= 149,
            CLOBBERBUILTINS= 150,
            EOF     = 151,
            WHITESPACE= 152,
            COMMENTLINE= 153
    }

    public class Token
    {
        private string file;
        private int line;
        private int column;
        private int startpos;
        private int endpos;
        private string text;
        private object value;

        // contains all prior skipped symbols
        private List<Token> skipped;

        public string File { 
            get { return file; } 
            set { file = value; }
        }

        public int Line { 
            get { return line; } 
            set { line = value; }
        }

        public int Column {
            get { return column; } 
            set { column = value; }
        }

        public int StartPos { 
            get { return startpos;} 
            set { startpos = value; }
        }

        public int Length { 
            get { return endpos - startpos;} 
        }

        public int EndPos { 
            get { return endpos;} 
            set { endpos = value; }
        }

        public string Text { 
            get { return text;} 
            set { text = value; }
        }

        public List<Token> Skipped { 
            get { return skipped;} 
            set { skipped = value; }
        }
        public object Value { 
            get { return value;} 
            set { this.value = value; }
        }

        [XmlAttribute]
        public TokenType Type;

        public Token()
            : this(0, 0)
        {
        }

        public Token(int start, int end)
        {
            Type = TokenType._UNDETERMINED_;
            startpos = start;
            endpos = end;
            Text = ""; // must initialize with empty string, may cause null reference exceptions otherwise
            Value = null;
        }

        public void UpdateRange(Token token)
        {
            if (token.StartPos < startpos) startpos = token.StartPos;
            if (token.EndPos > endpos) endpos = token.EndPos;
        }

        public override string ToString()
        {
            if (Text != null)
                return Type.ToString() + " '" + Text + "'";
            else
                return Type.ToString();
        }
    }

    #endregion
}
