﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SISTEMA_AULA.MODEL.Models;

public partial class ItensVendum
{
    public int ItvCodigoVenda { get; set; }

    public int ItvCodigoProduto { get; set; }

    public int ItvQuantidade { get; set; }
    public decimal ItvValorItem { get; set; }

    public virtual Produto ItvCodigoProdutoNavigation { get; set; }

    public virtual Vendum ItvCodigoVendaNavigation { get; set; }
}