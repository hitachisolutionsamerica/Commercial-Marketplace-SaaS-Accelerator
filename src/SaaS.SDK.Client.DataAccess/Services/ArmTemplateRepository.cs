﻿using Microsoft.Marketplace.SaasKit.Client.DataAccess.Context;
using Microsoft.Marketplace.SaasKit.Client.DataAccess.Contracts;
using Microsoft.Marketplace.SaasKit.Client.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Marketplace.SaasKit.Client.DataAccess.Services
{
    public class ArmTemplateRepository : IArmTemplateRepository
    {

        private readonly SaasKitContext Context;

        public ArmTemplateRepository(SaasKitContext context)
        {
            Context = context;
        }

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ARMTemplates> Get()
        {
            return Context.Armtemplates;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ARMTemplates Get(int id)
        {
            return Context.Armtemplates.Where(s => s.ArmtempalteId == id).FirstOrDefault();
        }


        /// <summary>
        /// Adds the specified Offer details.
        /// </summary>
        /// <param name="offerDetails">The Offers details.</param>
        /// <returns></returns>
        public int Add(ARMTemplates templateDetails)
        {
            if (templateDetails != null && !string.IsNullOrEmpty(templateDetails.ArmtempalteName))
            {
                var existingTemplate = Context.Armtemplates.Where(s => s.ArmtempalteName == templateDetails.ArmtempalteName).FirstOrDefault();
                if (existingTemplate != null)
                {
                    existingTemplate.ArmtempalteName = templateDetails.ArmtempalteName;
                    existingTemplate.TemplateLocation = templateDetails.TemplateLocation;
                    Context.Armtemplates.Update(existingTemplate);
                    Context.SaveChanges();
                    return existingTemplate.ArmtempalteId;
                }
                else
                {
                    Context.Armtemplates.Add(templateDetails);
                    Context.SaveChanges();
                    return templateDetails.ArmtempalteId;
                }
            }
            return 0;
        }

        /// <summary>
        /// Removes the specified plan details.
        /// </summary>
        /// <param name="offerDetails">The offer details.</param>
        public void Remove(ARMTemplates templateDetails)
        {
            var existingTemplate = Context.Armtemplates.Where(s => s.ArmtempalteId == templateDetails.ArmtempalteId).FirstOrDefault();
            if (existingTemplate != null)
            {
                Context.Armtemplates.Remove(existingTemplate);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
