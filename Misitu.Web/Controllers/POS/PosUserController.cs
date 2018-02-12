using Misitu.POSUser;
using Misitu.POSUser.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers.POS
{
    public class PosUserController : MisituControllerBase
    {
        private readonly ICheckpointUserAppService checkpoitUserAppService;

        public PosUserController(ICheckpointUserAppService checkpoitUserAppService)
        {
            this.checkpoitUserAppService = checkpoitUserAppService;
        }


        // GET: PosUser
        public ActionResult Index()
        {
            var ucs = this.checkpoitUserAppService.GetCheckpoitUsers();
            return View(ucs);
        }

        // GET: PosUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PosUser/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: PosUser/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateCheckpointUser input)
        {
            try
            {
                // TODO: Add insert logic here
                await this.checkpoitUserAppService.CreateCheckpointUser(input);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PosUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PosUser/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PosUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PosUser/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
