using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Eshop.Prodotti;

public class ProdottoAlreadyExistsException : BusinessException
{
    public ProdottoAlreadyExistsException(string name) : base(EshopDomainErrorCodes.ProdottoAlreadyExists)
    {
        WithData("name", name);
    }
}