﻿// -----------------------------------------------------------------------
// <copyright file="Matter.cs" company="Nodine Legal, LLC">
// Licensed to Nodine Legal, LLC under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  Nodine Legal, LLC licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
// </copyright>
// -----------------------------------------------------------------------

namespace OpenLawOffice.Data.Matters
{
    using System;
    using AutoMapper;
    using System.Data;
    using System.Collections.Generic;
    using Dapper;
    using System.Linq;
    
    public static class CourtGeographicalJurisdiction
    {
        public static Common.Models.Matters.CourtGeographicalJurisdiction Get(
            int id,
            IDbConnection conn = null,
            bool closeConnection = true)
        {
            return DataHelper.Get<Common.Models.Matters.CourtGeographicalJurisdiction, DBOs.Matters.CourtGeographicalJurisdiction>(
                "SELECT * FROM \"court_geographical_jurisdiction\" WHERE \"id\"=@id AND \"utc_disabled\" is null",
                new { id = id }, conn, closeConnection);
        }

        public static Common.Models.Matters.CourtGeographicalJurisdiction Get(
            Transaction t,
            int id)
        {
            return Get(id, t.Connection, false);
        }

        public static List<Common.Models.Matters.CourtGeographicalJurisdiction> List(
            IDbConnection conn = null,
            bool closeConnection = true)
        {
            return DataHelper.List<Common.Models.Matters.CourtGeographicalJurisdiction, DBOs.Matters.CourtGeographicalJurisdiction>(
                "SELECT * FROM \"court_geographical_jurisdiction\" WHERE \"utc_disabled\" is null", null, conn, closeConnection);
        }

        public static List<Common.Models.Matters.CourtGeographicalJurisdiction> List(
            Transaction t)
        {
            return List(t.Connection, false);
        }

        public static Common.Models.Matters.CourtGeographicalJurisdiction Create(
            Common.Models.Matters.CourtGeographicalJurisdiction model,
            Common.Models.Account.Users creator,
            IDbConnection conn = null,
            bool closeConnection = true)
        {
            model.CreatedBy = model.ModifiedBy = creator;
            model.Created = model.Modified = DateTime.UtcNow;
            DBOs.Matters.CourtGeographicalJurisdiction dbo = Mapper.Map<DBOs.Matters.CourtGeographicalJurisdiction>(model);

            conn = DataHelper.OpenIfNeeded(conn);

            if (conn.Execute("INSERT INTO \"court_geographical_jurisdiction\" (\"title\", \"utc_created\", \"utc_modified\", \"created_by_user_pid\", \"modified_by_user_pid\") " +
                "VALUES (@Title, @UtcCreated, @UtcModified, @CreatedByUserPId, @ModifiedByUserPId)",
                dbo) > 0)
                model.Id = conn.Query<DBOs.Matters.CourtGeographicalJurisdiction>("SELECT currval(pg_get_serial_sequence('court_geographical_jurisdiction', 'id')) AS \"id\"").Single().Id;

            DataHelper.Close(conn, closeConnection);

            return model;
        }

        public static Common.Models.Matters.CourtGeographicalJurisdiction Create(
            Transaction t,
            Common.Models.Matters.CourtGeographicalJurisdiction model,
            Common.Models.Account.Users creator)
        {
            return Create(model, creator, t.Connection, false);
        }

        public static Common.Models.Matters.CourtGeographicalJurisdiction Edit(
            Common.Models.Matters.CourtGeographicalJurisdiction model,
            Common.Models.Account.Users modifier,
            IDbConnection conn = null,
            bool closeConnection = true)
        {
            model.ModifiedBy = modifier;
            model.Modified = DateTime.UtcNow;
            DBOs.Matters.CourtGeographicalJurisdiction dbo = Mapper.Map<DBOs.Matters.CourtGeographicalJurisdiction>(model);

            conn = DataHelper.OpenIfNeeded(conn);

            conn.Execute("UPDATE \"court_geographical_jurisdiction\" SET " +
                "\"title\"=@Title, \"utc_modified\"=@UtcModified, \"modified_by_user_pid\"=@ModifiedByUserPId " +
                "WHERE \"id\"=@Id", dbo);

            DataHelper.Close(conn, closeConnection);

            return model;
        }

        public static Common.Models.Matters.CourtGeographicalJurisdiction Edit(
            Transaction t,
            Common.Models.Matters.CourtGeographicalJurisdiction model,
            Common.Models.Account.Users modifier)
        {
            return Edit(model, modifier, t.Connection, false);
        }

        public static Common.Models.Matters.CourtGeographicalJurisdiction Disable(
            Common.Models.Matters.CourtGeographicalJurisdiction model,
            Common.Models.Account.Users disabler,
            IDbConnection conn = null,
            bool closeConnection = true)
        {
            model.DisabledBy = disabler;
            model.Disabled = DateTime.UtcNow;

            DataHelper.Disable<Common.Models.Matters.CourtGeographicalJurisdiction,
                DBOs.Matters.CourtGeographicalJurisdiction>("court_geographical_jurisdiction", disabler.PId.Value, model.Id, conn, closeConnection);

            return model;
        }

        public static Common.Models.Matters.CourtGeographicalJurisdiction Disable(
            Transaction t,
            Common.Models.Matters.CourtGeographicalJurisdiction model,
            Common.Models.Account.Users disabler)
        {
            return Disable(model, disabler, t.Connection, false);
        }
    }
}
