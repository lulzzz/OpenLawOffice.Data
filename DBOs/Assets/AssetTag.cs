﻿// -----------------------------------------------------------------------
// <copyright file="AssetTag.cs" company="Nodine Legal, LLC">
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

namespace OpenLawOffice.Data.DBOs.Assets
{
    using System;
    using AutoMapper;

    /// <summary>
    /// </summary>
    [Common.Models.MapMe]
    public class AssetTag : Core // Table : asset_asset_tag
    {
        [ColumnMapping(Name = "id")]
        public Guid Id { get; set; }

        [ColumnMapping(Name = "asset_id")]
        public Guid AssetId { get; set; }

        [ColumnMapping(Name = "asset_tag_id")]
        public int TagId { get; set; }

        public void BuildMappings()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(AssetTag), new ColumnAttributeTypeMapper<AssetTag>());
            Mapper.CreateMap<DBOs.Assets.AssetTag, Common.Models.Assets.AssetTag>()
                .ForMember(dst => dst.IsStub, opt => opt.UseValue(false))
                .ForMember(dst => dst.Created, opt => opt.ResolveUsing(db =>
                {
                    return db.UtcCreated.ToSystemTime();
                }))
                .ForMember(dst => dst.Modified, opt => opt.ResolveUsing(db =>
                {
                    return db.UtcModified.ToSystemTime();
                }))
                .ForMember(dst => dst.Disabled, opt => opt.ResolveUsing(db =>
                {
                    return db.UtcDisabled.ToSystemTime();
                }))
                .ForMember(dst => dst.CreatedBy, opt => opt.ResolveUsing(db =>
                {
                    return new Common.Models.Account.Users()
                    {
                        PId = db.CreatedByUserPId,
                        IsStub = true
                    };
                }))
                .ForMember(dst => dst.ModifiedBy, opt => opt.ResolveUsing(db =>
                {
                    return new Common.Models.Account.Users()
                    {
                        PId = db.ModifiedByUserPId,
                        IsStub = true
                    };
                }))
                .ForMember(dst => dst.DisabledBy, opt => opt.ResolveUsing(db =>
                {
                    if (!db.DisabledByUserPId.HasValue) return null;
                    return new Common.Models.Account.Users()
                    {
                        PId = db.DisabledByUserPId.Value,
                        IsStub = true
                    };
                }))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Asset, opt => opt.ResolveUsing(db =>
                {
                    return new Common.Models.Assets.Asset()
                    {
                        Id = db.AssetId,
                        IsStub = true
                    };
                }))
                .ForMember(dst => dst.Tag, opt => opt.ResolveUsing(db =>
                {
                    return new Common.Models.Assets.Tag()
                    {
                        Id = db.TagId,
                        IsStub = true
                    };
                }));

            Mapper.CreateMap<Common.Models.Assets.AssetTag, DBOs.Assets.AssetTag>()
                .ForMember(dst => dst.UtcCreated, opt => opt.ResolveUsing(db =>
                {
                    return db.Created.ToDbTime();
                }))
                .ForMember(dst => dst.UtcModified, opt => opt.ResolveUsing(db =>
                {
                    return db.Modified.ToDbTime();
                }))
                .ForMember(dst => dst.UtcDisabled, opt => opt.ResolveUsing(db =>
                {
                    return db.Disabled.ToDbTime();
                }))
                .ForMember(dst => dst.CreatedByUserPId, opt => opt.ResolveUsing(model =>
                {
                    if (model.CreatedBy == null || !model.CreatedBy.PId.HasValue)
                        return Guid.Empty;
                    return model.CreatedBy.PId.Value;
                }))
                .ForMember(dst => dst.ModifiedByUserPId, opt => opt.ResolveUsing(model =>
                {
                    if (model.ModifiedBy == null || !model.ModifiedBy.PId.HasValue)
                        return Guid.Empty;
                    return model.ModifiedBy.PId.Value;
                }))
                .ForMember(dst => dst.DisabledByUserPId, opt => opt.ResolveUsing(model =>
                {
                    if (model.DisabledBy == null) return null;
                    return model.DisabledBy.PId;
                }))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.AssetId, opt => opt.ResolveUsing(model =>
                {
                    if (model.Asset != null)
                        return model.Asset.Id;
                    else
                        return null;
                }))
                .ForMember(dst => dst.TagId, opt => opt.ResolveUsing(model =>
                {
                    if (model.Tag != null)
                        return model.Tag.Id;
                    else
                        return null;
                }));
        }
    }
}
