using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace AgendaDeEventos.Data.SearchModels
{
    public class UsuarioSearch : IQuerySort, IQueryPaging
    {
        [QueryOperator(Operator = WhereOperator.Contains)]
        public virtual string Nome { get; set; }
        
        [QueryOperator(Operator = WhereOperator.Contains)]
        public virtual string Email { get; set; }
        
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string Sort { get; set; }
    }
}