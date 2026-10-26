namespace eval;
class Token { }
class Tnumber :  Token { }
class Tvariable  : Tnumber { }
class Tliteral  : Tnumber { }
class Toperator : Token  { }
class TopBinary : Toperator { }
class  TopFunction : Toperator { }
class Tpunctuation : Token { }   
class Tend : Token { }
class Terror : Token { }