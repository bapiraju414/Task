﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManager.DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TaskDBEntities : DbContext
    {
        public TaskDBEntities()
            : base("name=TaskDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<ParentTask> ParentTasks { get; set; }
    
        public virtual ObjectResult<GetTaskDetails_Result> GetTaskDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTaskDetails_Result>("GetTaskDetails");
        }
    }
}
