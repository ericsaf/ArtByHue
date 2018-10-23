using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    public class WebReferencesController : Controller
    {
        private readonly ArtByHueContext _context;

        public WebReferencesController(ArtByHueContext context)
        {
            _context = context;    
        }

        // GET: WebReferences
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebReferences.ToListAsync());
        }

        // GET: WebReferences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webReference = await _context.WebReferences.Include(x => x.Pictures).SingleOrDefaultAsync(m => m.WebReferenceID == id);
            if (webReference == null)
            {
                return NotFound();
            }

            return View(webReference);
        }

        // GET: WebReferences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebReferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WebReferenceID,Comments,EntryDate,HatTipLink,HatTipName,HatTipSourceLink,HatTipSourceName,SourceLink,SourceViaText,Title,UploadFiles,SimilarProducts")] WebReference webReference)
        {
            if (ModelState.IsValid)
            {
                if (webReference.UploadFiles?.Count > 0)
                {
                    webReference.Pictures = new List<WebReferenceImage>();
                }

                foreach (var file in webReference.UploadFiles)
                {
                    var stream = file.OpenReadStream();
                    var name = file.FileName;

                    var uploadedFile = new WebReferenceImage
                    {
                        ImageURL= await UploadFileAsBlob(stream, name)
                    };

                    webReference.Pictures.Add(uploadedFile);
                }
                _context.Add(webReference);
                await _context.SaveChangesAsync();
                return Redirect("/WebReferences/Index");
            }
            return View(webReference);
        }

        public async Task<string> UploadFileAsBlob(Stream stream, string filename)
        {
            
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=artbyhue;AccountKey=dWHMVqBrrr0pp34pUd+pvniY8oH1ht1ESy7NND7aR7xVOKrT7qIjlhDHzKrlG9elw4ya4yJIjXY2boLVHNmXrw==;");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("web-images");

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);

            await blockBlob.UploadFromStreamAsync(stream);

            stream.Dispose();
            return blockBlob?.Uri.ToString();
        }

        // GET: WebReferences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webReference = await _context.WebReferences.Include(x => x.Pictures).Include(x => x.SimilarProducts).SingleOrDefaultAsync(m => m.WebReferenceID == id);
            if (webReference == null)
            {
                return NotFound();
            }
            return View(webReference);
        }

        // POST: WebReferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WebReferenceID,Comments,EntryDate,HatTipLink,HatTipName,HatTipSourceLink,HatTipSourceName,SourceLink,SourceViaText,Title,Pictures,SimilarProducts")] WebReference webReference)
        {
            if (id != webReference.WebReferenceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webReference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebReferenceExists(webReference.WebReferenceID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/WebReferences/Index");
            }
            return View(webReference);
        }

        // GET: WebReferences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webReference = await _context.WebReferences.SingleOrDefaultAsync(m => m.WebReferenceID == id);
            if (webReference == null)
            {
                return NotFound();
            }

            return View(webReference);
        }

        // POST: WebReferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webReference = await _context.WebReferences.SingleOrDefaultAsync(m => m.WebReferenceID == id);
            _context.WebReferences.Remove(webReference);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WebReferenceExists(int id)
        {
            return _context.WebReferences.Any(e => e.WebReferenceID == id);
        }
    }
}
