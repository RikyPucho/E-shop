using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;

namespace Eshop;

[BlobContainerName("ImmaginiProdotti")]
public class ImmaginiContainer
{
    Guid ProdId { get; set; }

    ImmaginiContainer(Guid prodId) 
    { 
        ProdId = prodId;
    }
}
