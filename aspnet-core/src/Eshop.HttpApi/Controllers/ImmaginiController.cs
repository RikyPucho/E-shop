using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;

namespace Eshop.Controllers
{
    [Controller]
    [Route ("api/img")]
    public class ImmaginiController : EshopController
    {
        private readonly IBlobContainer<ImmaginiContainer> _blobContainer;
        public ImmaginiController(IBlobContainer<ImmaginiContainer> blobContainer)
        {
            _blobContainer = blobContainer;
        }

        [HttpPost("upload")]
        [AllowAnonymous]
        public async Task<byte[]> UploadAsync(IRemoteStreamContent img)
        {
            if(img == null)
            {
                throw new ArgumentNullException(nameof(img));
            }
            var stream = img.GetStream();
            var bytes = await stream.GetAllBytesAsync();
            var name = img.FileName;

            await _blobContainer.SaveAsync(name, bytes);

            return bytes;
        }

        [HttpGet("")]
        public async Task<byte[]> GetImageAsync(string name)
        {
            return await _blobContainer.GetAllBytesAsync(name);
        }
    }
}
